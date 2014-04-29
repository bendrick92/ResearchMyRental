using RateMyRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace RateMyRental.Models
{
    public class Resources_IndexViewModel
    {
        public IEnumerable<ResourceHeading> resourceHeadings { get; set; }
        public IEnumerable<Resource> resources { get; set; }
    }

    public class AddResourceViewModel
    {
        public Resource resource { get; set; }
        public List<SelectListItem> resourceHeadingsList { get; set; }
        public bool isURL { get; set; }
    }

    public class EditResourceViewModel
    {
        public Resource resource { get; set; }
        public List<SelectListItem> resourceHeadingsList { get; set; }
        public bool isURL { get; set; }
        public string oldFileName { get; set; }
    }

    public class DeleteResourceViewModel
    {
        public Resource resource { get; set; }
    }

    public class AddResourceHeadingViewModel
    {
        public List<string> currentResourceHeadingsList { get; set; }
        public ResourceHeading resourceHeading { get; set; }
        public string newResourceHeadingTitle { get; set; }
    }

    public class EditResourceHeadingViewModel
    {
        public List<string> currentResourceHeadingsList { get; set; }
        public ResourceHeading resourceHeading { get; set; }
    }

    public class DeleteResourceHeadingViewModel
    {
        public ResourceHeading resourceHeading { get; set; }
    }
}