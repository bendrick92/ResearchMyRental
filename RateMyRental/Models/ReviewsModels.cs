using RateMyRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace RateMyRental.Models
{
    public class Reviews_IndexViewModel
    {
        public string quickSearchValue { get; set; }
    }

    public class AddPropertyViewModel
    {
        public Property property { get; set; }
        public string propertyPicture { get; set; }
        public List<SelectListItem> statesList { get; set; }
        public List<SelectListItem> citiesList { get; set; }
    }
}