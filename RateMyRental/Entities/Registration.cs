using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyRental.Entities
{
    public class Registration
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public string token { get; set; }
    }
}