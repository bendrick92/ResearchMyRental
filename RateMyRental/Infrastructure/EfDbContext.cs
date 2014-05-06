using RateMyRental.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RateMyRental.Infrastructure
{
    public class EfDbContext : DbContext
    {
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<PasswordResetRequest> PasswordResetRequests { get; set; }
        public DbSet<ResourceHeading> ResourceHeadings { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }

        //Link to connection in web.config
        public EfDbContext()
            : base("name=defaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}