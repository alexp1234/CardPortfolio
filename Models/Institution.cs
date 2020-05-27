using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class Institution
    {
        public string Name { get; set; } // 
        public int Id { get; set; } 
        public InstitutionType InstitutionType { get; set; } 
        public string ImagePath { get; set; } //
        public string Description { get; set; } //

        //04/09
        public bool AllowsBalanceTransferCash { get; set; }  //

        //04/10
        public bool HasRestrictionsToJoin { get; set; } //..
        public RestrictionType RestrictionType { get; set; } //..

        public double? LengthBetweenCreditLineIncreases { get; set; } //..

        public CreditBureau CreditBureau { get; set; } // ..
        public double? ScoreForLowestAPR { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }


        public DateTime? UpdatedDate { get; set; }
        public Institution()
        {
            CreditCards = new HashSet<CreditCard>();

        }
    }
}
