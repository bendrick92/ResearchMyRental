using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RateMyRental.Entities
{
    public class Property
    {
        public int ID { get; set; }
        [Required(ErrorMessage="House Number is required.")]
        public int HouseNum { get; set; }
        [Required(ErrorMessage="Street is required.")]
        public string Street { get; set; }
        public int AptNum { get; set; }
        [Required(ErrorMessage="Please select a city.")]
        public int CityID { get; set; }
        [NotMapped]
        public City City { get; set ;}
        [Required(ErrorMessage="Please select a state.")]
        public int StateID { get; set; }
        [NotMapped]
        public State State { get; set; }
        [Required(ErrorMessage="Please enter a zip.")]
        public int Zip { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsActive { get; set; }
    }
}