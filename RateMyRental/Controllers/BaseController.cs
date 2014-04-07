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

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (Request.IsAuthenticated && HttpContext.Session.IsNewSession)
            {
                Logout();
                Response.Redirect("~/Home");
            }
        }

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
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
	}
}