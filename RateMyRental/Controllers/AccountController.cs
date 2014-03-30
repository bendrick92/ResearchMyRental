using RateMyRental.Entities;
using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RateMyRental.Controllers
{
    public class AccountController : BaseController
    {

        [AllowAnonymous]
        public ActionResult Index()
        {
            Account_IndexViewModel model = new Account_IndexViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(Account_IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Does provided username exist in database?
                //Get user object from database based on requested e-mail/username
                User user = repo.GetUserByUsername(model.username);

                if (user != null)
                {
                    //Is there a password entered in the field?
                    if (model.password != "" && model.password != null)
                    {
                        //Hashed password from database
                        string hashedPassword = user.password;
                        //Does hashed password from database match hashed password in password field?
                        if (PasswordHash.PasswordHash.ValidatePassword(model.password, hashedPassword))
                        {
                            //Create session cookie
                            FormsAuthentication.SetAuthCookie(model.username, false);
                            //Login success
                            return RedirectToAction("Index", "Home", null);
                        }
                        //Passwords do not match
                        else
                        {
                            ModelState.AddModelError("password", "Invalid username or password.");
                        }
                    }
                    //No password entered
                    else
                    {
                        ModelState.AddModelError("password", "Invalid username or password.");
                    }
                }
                //No user from database tied to provided e-mail
                else
                {
                    ModelState.AddModelError("username", "Invalid username or password.");
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.user = new Entities.User();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Check that the provided e-mail conforms to domain restrictions
                string email = model.user.email;
                int index = email.IndexOf("@");
                if (index > 0)
                {
                    int remainder = email.Trim().Length - index;
                    string domain = email.Substring(index, remainder);

                    List<string> domains = repo.GetAllDomainNames();

                    //Provided e-mail conforms to domain restrictions
                    if (domains.Contains(domain.Trim().ToLower()))
                    {
                        //Check to be sure passwords match
                        if (model.user.password == model.password_confirm)
                        {
                            //Check to be sure e-mail is not already registered with system
                            if (repo.CheckIfEmailInUse(email) == false)
                            {
                                //Check to be sure terms and conditions are accepted
                                if (model.termsAccept == true)
                                {
                                    //Generate salt and encrypt password
                                    string hash = PasswordHash.PasswordHash.CreateHash(model.user.password);

                                    //Set password of model.user
                                    model.user.password = hash;

                                    //Set registration date to now
                                    model.user.registrationDate = DateTime.Now;

                                    //Add model.user to database
                                    repo.AddUser(model.user);

                                    //Generate new random token
                                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                                    //Create new registration object
                                    Registration registration = new Registration();

                                    //Set token of registration
                                    registration.token = token;

                                    //Set userID of model.registration to newly created User
                                    registration.userID = model.user.ID;

                                    //Add model.registration to database
                                    repo.AddRegistration(registration);

                                    //Send registration e-mail
                                    List<string> recipients = new List<string>();
                                    recipients.Add(email);
                                    string subject = "ResearchMyRental - Activate Account";
                                    string introduction = "Dear user,";
                                    string body = String.Format("This e-mail is to confirm your recent registration with ResearchMyRental.com.<br><br>To complete your registration and begin using the site, please click the link below.<br><br><a href=\"http://localhost:50737/Account/ActivateAccount?token=" + registration.token + "\">Activate your account</a>");
                                    Email.sendMessage(recipients, subject, introduction, body);

                                    //Return popup asking to confirm registration e-mail
                                    TempData["popupMessage"] = "Thank you!  Please check the e-mail account you provided to confirm your registration.";

                                    return RedirectToAction("Index", "Account", null);
                                }

                                //Terms and conditions were not accepted
                                else
                                {
                                    ModelState.AddModelError("termsAccept", "Please accept the Terms and Conditions.");
                                }
                            }
                            //E-mail address is already in use
                            else
                            {
                                ModelState.AddModelError("user.email", "This e-mail address is already registered with the system.");
                            }
                        }

                        //Passwords don't match
                        else
                        {
                            ModelState.AddModelError("password_confirm", "Passwords do not match");
                        }
                    }

                    //Return error that e-mail domain is not authorized to access the program
                    else
                    {
                        ModelState.AddModelError("user.email", "The provided e-mail domain is not authorized at this time");
                    }
                }
                else
                {
                    ModelState.AddModelError("user.email", "Invalid e-mail address");
                }
            }

            //Return view with validation messages
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult ActivateAccount(string token)
        {
            ActivateAccountViewModel model = new ActivateAccountViewModel();
            //Retrieve Registration object from token parameter
            Registration registration = repo.GetRegistrationByToken(token);
            //Registration hasn't been resolved
            if (registration != null)
            {
                User user = repo.GetUserByID(registration.userID);
                repo.ActivateUser(user.ID);
                //Delete entry from Registration table - no longer needed
                repo.DeleteRegistration(registration.ID);
                return View(model);
            }
            //Registration has already been resolved
            else
            {
                return RedirectToAction("Error", new { errorMessage = "This account has already been activated!" });
            }
        }
	}
}