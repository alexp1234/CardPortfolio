using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class DepositAccount
    {
        [Required]
        public double InterestRate { get; set; }
        public int? InstitutionId { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }
        
        // Minimum To Open Account
        public bool HasMinimumToOpenAccount { get; set; }
        public double? MinimumToOpenAccount { get; set; }

        // Monthly Fee Information
        public bool HasMonthlyFee { get; set; }

        [Required]
        public double MonthlyFee { get; set; }
        public double? BalanceToAvoidFee { get; set; }

        // Min-Max for APR
        public bool HasMinimumAmountForApr { get; set; }
        public double? MinimumAmountForApr { get; set; }
        public bool HasMaximumAmountForApr { get; set; }
        public double? MaximumAmountForApr { get; set; }

        // If Minimum or Maximum Amount is not met
        public double? AprIfAmountNotMet {get; set;}

        
        

    }
}
