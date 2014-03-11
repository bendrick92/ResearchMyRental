using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyRental.Controllers
{
    public class HomeController : BaseController
    {
        /// <summary>
        /// Render Index View for Home
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Home_IndexViewModel model = new Home_IndexViewModel();
            return View(model);
        }
	}
}