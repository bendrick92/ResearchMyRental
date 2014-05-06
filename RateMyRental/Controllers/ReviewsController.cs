using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyRental.Controllers
{
    public class ReviewsController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            Reviews_IndexViewModel model = new Reviews_IndexViewModel();
            return View();
        }

        public ActionResult AddProperty()
        {
            AddPropertyViewModel model = new AddPropertyViewModel();
            model.property = new Entities.Property();
            model.statesList = repo.GetStatesList();
            model.citiesList = repo.GetCitiesList();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddProperty(AddPropertyViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Was a Property picture uploaded?  (Optional, but must be valid)
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    foreach (string requestFile in Request.Files)
                    {
                        HttpPostedFileBase file = Request.Files[requestFile];
                        if (file.ContentLength > 0)
                        {
                            //Picture was uploaded
                            FileInfo fi = new FileInfo(file.FileName);

                            //Is picture a valid file?
                            List<string> validExtensions = new List<string>();
                            validExtensions.Add(".jpg");
                            validExtensions.Add(".png");
                            validExtensions.Add(".gif");
                            validExtensions.Add(".bmp");
                            
                            //Get extension of uploaded file
                            string extension = fi.Extension.ToLower();

                            //If the uploaded file is greater in size than 10 GB, it will throw a "Max request length exceeded" error
                            //This is unavoidable without redirecting on error code (404 specific to this error) which I haven't found
                            if (validExtensions.Contains(extension))
                            {
                                //File is valid type
                                string fileName = model.property.ID.ToString();

                                //Set picture name to ID of property - prevent duplications
                                var path = Path.Combine(Server.MapPath("~/Content/PropertyImages"), fileName);
                                file.SaveAs(path);

                            }
                            //File is not valid
                            else
                            {
                                //Add error and return
                                ModelState.AddModelError("propertyPicture", "Picture type is invalid.  Please use a valid file.");
                                return View(model);
                            }
                        }
                    }
                }
                //If no picture is uploaded, the above code is simply skipped and the Property is added to the db
                //Finalize Property data
                model.property.IsActive = false;
                model.property.DateAdded = DateTime.Now;

                //Add Property to db
                repo.AddProperty(model.property);

                //Notify administrators that a Property needs to be approved via e-mail
                List<string> recipients = repo.GetAdminEmailsList();
                string subject = "ResearchMyRental - New Property";
                string introduction = "Dear administrator,";
                // NEED TO ADD TOKEN SYSTEM AND PROPERTY REQUEST SYSTEM - KEEP USERS FROM APPROVING THEIR OWN LISTINGS
                string body = String.Format("This e-mail is to notify you that a new property needs approval on ResearchMyRental.com.<br><br>To approve or deny the listing of this property, please click the link below.<br><br>");
                Email.sendMessage(recipients, subject, introduction, body);

                //Return popup asking to confirm registration e-mail
                TempData["popupMessage"] = "Thank you!  Please check the e-mail account you provided to confirm your registration.";

                return RedirectToAction("Login", "Account", null);

            }

            return View(model);
        }
	}
}