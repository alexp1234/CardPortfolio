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

        
        public DbSet<AutoLoan> AutoLoans { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<HomeEquityLineOfCredit> HomeEquityLinesOfCredit { get; set; }
        public DbSet<HomeEquityLoan> HomeEquityLoans { get; set; }
        public DbSet<Mortgage> Mortgages { get; set; }
        public DbSet<SecuredLineOfCredit> SecuredLinesOfCredit { get; set; }
        public DbSet<SecuredPersonalLoan> SecuredPersonalLoans { get; set; }
        public DbSet<UnsecuredLineOfCredit> UnsecuredLinesOfCredit { get; set; }
        public DbSet<UnsecuredPersonalLoan> UnsecuredPersonalLoans { get; set; }
        public DbSet<CertificateAccount> CertificateAccounts { get; set; }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<MoneyMarketAccount> MoneyMarketAccounts { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        


    }
}
