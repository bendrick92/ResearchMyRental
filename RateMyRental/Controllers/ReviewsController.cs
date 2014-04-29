using RateMyRental.Models;
using System;
using System.Collections.Generic;
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
	}
}