using Microsoft.EntityFrameworkCore;
using RegisterApp.Models.Entities;
using System;

namespace RegisterApp.DAL
{
    public class RegisterAppContext : DbContext
    {
        public RegisterAppContext(DbContextOptions<RegisterAppContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTable>()
                .HasIndex(customer => customer.CustomerID).IsUnique();
        }

        public DbSet<CustomerTable> Users { get; set; }

    }
}
