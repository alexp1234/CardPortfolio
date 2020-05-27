using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class SecuredPersonalLoan: Loan
    {
        public int Id { get; set; }
        public double? CollateralToLoanRatio { get; set; }

    }
}
