using CardPortfolio.Models;
using CardPortfolio.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.ViewModels
{
    public class AddInstitutionImageViewModel
    {
        public string Name { get; set; } // 
        public int Id { get; set; }
        public InstitutionType InstitutionType { get; set; }
        public IFormFile ImagePath { get; set; } //
        public string Description { get; set; } //

        public bool AllowsBalanceTransferCash { get; set; }  //


        public bool HasRestrictionsToJoin { get; set; } //..
        public RestrictionType RestrictionType { get; set; } //..

        public double? LengthBetweenCreditLineIncreases { get; set; } //..

        public CreditBureau CreditBureau { get; set; } // ..
        public double? ScoreForLowestAPR { get; set; }
        public List<CreditCard> CreditCards { get; set; }


        public DateTime? UpdatedDate { get; set; }

    }
}
