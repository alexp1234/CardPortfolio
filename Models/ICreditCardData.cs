using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface ICreditCardData
    {
        IEnumerable<CreditCard> GetCreditCardsByCardNetwork(CardNetwork cardNetwork);
        IEnumerable<CreditCard> GetAllCreditCards();
        IEnumerable<CreditCard> GetAllCreditCardsByInstitutionId(int id);
        IQueryable<CreditCard> GetBestBalanceTransferCardsByRate();
        IEnumerable<CreditCard> GetAllCreditCardsWithNoInstitutionId();
        IQueryable<CreditCard> LowestRegularBalanceTransferCards();
        IQueryable<CreditCard> GetLongestIntroBalanceTransferCards(); //
        IQueryable<CreditCard> NoBalanceTransferFeeCards();
        IQueryable<CreditCard> NoCashAdvanceFeeCards();
        IQueryable<CreditCard> CashSignUpBonusCards(); //
        IQueryable<CreditCard> AirlineSignUpBonusCards();
        IQueryable<CreditCard> StorePointsBonusCards();
        IQueryable<CreditCard> HotelPointsBonusCards();
        IQueryable<CreditCard> CruisePointsBonusCards();
        IQueryable<CreditCard> LowestCashAdvanceAPRCards();
        CreditCard GetById(int id);
        int Add(CreditCard creditCard);
        int Delete(CreditCard creditCard);
        int GetCountOfCreditCards();
        int Commit();
    }
}
