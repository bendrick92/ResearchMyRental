using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateMyRental.Entities
{
    public class Resource
    {
        public int ID { get; set; }
        public string DisplayName { get; set; }
        public string FileName { get; set; }
        public bool IsURL { get; set; }
        public int ResourceType { get; set; }
        public DateTime UploadDate { get; set; }

        [NotMapped]
        public ResourceHeading ResourceHeading { get; set; }
    }
}