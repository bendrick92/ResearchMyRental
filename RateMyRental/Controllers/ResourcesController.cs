using RateMyRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RateMyRental.Controllers
{
    public class ResourcesController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            Resources_IndexViewModel model = new Resources_IndexViewModel();
            model.resourceHeadings = repo.GetAllResourceHeadings();
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AddResource()
        {

        }
	}
}