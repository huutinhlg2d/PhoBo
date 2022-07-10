using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhoBo.Models;

namespace PhoBo.Data
{
    public class PhoBoContext : DbContext
    {
        public PhoBoContext (DbContextOptions<PhoBoContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Photographer>().ToTable("Photographer");
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }

        public DbSet<PhoBo.Models.User> User { get; set; }
        public DbSet<PhoBo.Models.Photographer> Photographer { get; set; }
        public DbSet<PhoBo.Models.Customer> Customer { get; set; }
        public DbSet<PhoBo.Models.BookingConceptConfig> BookingConceptConfig { get; set; }
        public DbSet<PhoBo.Models.BookingConceptImage> BookingConceptImage { get; set; }
        public DbSet<PhoBo.Models.Concept> Concept { get; set; }
        public DbSet<PhoBo.Models.Booking> Booking { get; set; }
    }
}
