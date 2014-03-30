using RateMyRental.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RateMyRental
{
    public class DbInitialize
    {
        public void Initialize()
        {
            Database.SetInitializer<EfDbContext>(null);
        }
    }
}