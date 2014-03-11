using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyRental.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Index()
        {
            Account_IndexViewModel model = new Account_IndexViewModel();
            return View(model);
        }
	}
}