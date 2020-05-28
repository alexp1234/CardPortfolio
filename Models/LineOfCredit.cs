using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class LineOfCredit
    {
        
        public double? LowApr { get; set; }//
        public double? HighApr { get; set; }//
        public int? InstitutionId { get; set; }
        public double? MinimumAmount { get; set; } // 
        public double? MaximumAmount { get; set; } //
        public int? MinimumTermInMonths { get; set; } //
        public int? MaximumTermInMonths { get; set; } //
        public string ImageUrl { get; set; }
        public bool HasOriginationFee { get; set; } //fee
        public double? OriginationFee { get; set; } //fee
        public bool HasAnnualFee { get; set; } // fee
        public double? AnnualFee { get; set; } // fee
        public bool HasAdvanceFee { get; set; } // fee
        public double? AdvanceFee { get; set; } //fee
        public string Name { get; set; }//
        public bool ArePaymentsInterestOnly { get; set; }
        public double? MinimumPayment { get; set; }
        
    }
}
