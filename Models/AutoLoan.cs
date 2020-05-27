using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class AutoLoan:Loan
    {
        public int Id { get; set; }
        public AutoLoanCategory AutoLoanCategory { get; set; }
        public double? DownPaymentPercentage { get; set; }


    }
}
