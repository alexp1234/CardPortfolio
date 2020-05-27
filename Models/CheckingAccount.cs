using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class CheckingAccount: DepositAccount
    {
        public int Id { get; set; }
        public bool HasDirectDepositRequirementForAPR { get; set; }
        public double? DirectDepositRequirementsForAPR { get; set; }
        public double? InterestRateIfDDRequirementsNotMet { get; set; }
        public double? DirectDepositToAvoidMonthlyFee { get; set; }
    }
}
