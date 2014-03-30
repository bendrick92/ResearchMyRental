using RateMyRental.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RateMyRental.Models
{
    public class Account_IndexViewModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class RegisterViewModel
    {
        public User user { get; set; }
        public string password_confirm { get; set; }
        public bool termsAccept { get; set; }
    }

    public class ActivateAccountViewModel
    {
        public User user { get; set; }
    }

    public class ResetPasswordViewModel
    {

    }
}