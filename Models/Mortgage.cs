using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class Mortgage:Loan
    {
        public int Id { get; set; }
        public double? LoanToValue { get; set; }
        public MortgageType MortgageType { get; set; }
        public double? DownPaymentPercentage { get; set; }

    }
}
