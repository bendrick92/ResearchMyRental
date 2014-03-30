using RateMyRental.DAL;
using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RateMyRental.Controllers
{
    public class BaseController : Controller
    {
        //Repsitory object used for DAL processes.
        public DbRepository repo = new DbRepository();

        [AllowAnonymous]
        public ActionResult Error(string errorMessage)
        {
            ErrorViewModel model = new ErrorViewModel();
            model.errorMessage = errorMessage;
            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Account");
        }
	}
}