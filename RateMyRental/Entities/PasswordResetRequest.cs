using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RateMyRental.Entities
{
    public class PasswordResetRequest
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Token { get; set; }
    }
}