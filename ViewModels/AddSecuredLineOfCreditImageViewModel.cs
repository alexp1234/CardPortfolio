using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.ViewModels
{
    public class AddSecuredLineOfCreditImageViewModel
    {
        public double? LowApr { get; set; }
        public double? HighApr { get; set; }
        public int? InstitutionId { get; set; }
        public double? MinimumAmount { get; set; }
        public double? MaximumAmount { get; set; }
        public int? MinimumTermInMonths { get; set; }
        public int? MaximumTermInMonths { get; set; }
        public IFormFile Image { get; set; }
        public bool HasOriginationFee { get; set; }
        public double? OriginationFee { get; set; }
        public bool HasAnnualFee { get; set; }
        public double? AnnualFee { get; set; }
        public bool HasAdvanceFee { get; set; }
        public double? AdvanceFee { get; set; }
        public string Name { get; set; }
        public bool ArePaymentsInterestOnly { get; set; }
        public double? MinimumPayment { get; set; }

        public int Id { get; set; }
        public double CollateralToLocRatio { get; set; }

    }
}
