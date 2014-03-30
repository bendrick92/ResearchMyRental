using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateMyRental.Entities
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage="Invalid e-mail")]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public bool isActive { get; set; }
        public DateTime registrationDate { get; set; }
    }
}