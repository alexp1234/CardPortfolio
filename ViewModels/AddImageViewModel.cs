using CardPortfolio.Models;
using CardPortfolio.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.ViewModels
{
    public class AddImageViewModel
    {
        public int Id { get; set; }
        public int? InstitutionId { get; set; }
        [StringLength(80)]
        public string Name { get; set; }
        public bool HasIntroPurchaseOffer { get; set; }
        public double? IntroPurchaseRate { get; set; }
        public double? IntroPurchaseLengthInMonths { get; set; }
        public bool HasIntroBalanceTransferOffer { get; set; }
        public double? IntroBalanceTransferRate { get; set; }
        public double? IntroBalanceTransferLengthInMonths { get; set; }
        public double? LowAPRPurchases { get; set; }
        public double? HighAPRPurchases { get; set; }
        public double? LowAPRBalanceTransfer { get; set; }
        public double? HighAPRBalanceTransfer { get; set; }
        public double? LowAPRCashAdvance { get; set; }
        public double? HighAPRCashAdvance { get; set; }

        public bool HasSignUpBonus { get; set; }
        public double? SignUpBonusSpendRequirement { get; set; }
        public double? SignUpBonusAmount { get; set; }

        // new 05/01
        public SignUpBonusCategory SignUpBonusCategory { get; set; }


        public CardNetwork CardNetwork { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public CardCategory CardCategory { get; set; }

        public double? AnnualFee { get; set; }

        // added 04/10
        public bool HasIntroBalanceTransferFee { get; set; }
        public double? IntroBalanceTransferFee { get; set; }
        public double? CashAdvanceFee { get; set; }
        public double? RegularBalanceTransferFee { get; set; }
        public bool IsFixedRate { get; set; }

        // added 04/12
        public double? MinimumCreditLine { get; set; }
        public double? MaximumCreditLine { get; set; }

        // Added 04/14/2020
        public bool HasCashBack { get; set; }
        

        // Added 04/15/2020
        public DateTime? UpdatedDate { get; set; }
        public double? MinimumPaymentPercent { get; set; }
        public decimal? MinimumPaymentInDollars { get; set; }




        // Added 04/26
        public double? ForeignTransactionFee { get; set; }
    }
}
