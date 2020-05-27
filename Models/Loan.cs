using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class Loan
    {
        public double? LowApr { get; set; } //
        public double? HighApr { get; set; } //
        public int? InstitutionId { get; set; }
        public double? MinimumAmount { get; set; }  //
        public double? MaximumAmount { get; set; }  //
        public int? MinimumTermInMonths { get; set; } //
        public int? MaximumTermInMonths { get; set; } //
        public string ImageUrl { get; set; }
        public bool HasFees { get; set; } 
        public double? OriginationFee { get; set; }
        public string Name { get; set; } //
    }
}
