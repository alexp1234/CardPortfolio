using System;
using System.Collections.Generic;
using System.Text;
using CardPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CardPortfolio.Data
{

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
                {Name = "Admin", NormalizedName = "Admin".ToUpper()});
        }


        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        

        


    }
}
