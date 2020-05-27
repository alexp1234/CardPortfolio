using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using CardPortfolio.Models.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class CreditCardData : ICreditCardData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public CreditCardData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        public int Add(CreditCard creditCard)
        {
            try
            {
                _db.CreditCards.Add(creditCard);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
            
            
        }


        public int Commit()
        {
            try
            {
                _db.SaveChanges();
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }

        }

        public int Delete(CreditCard creditCard)
        {
               try
                {
                    _db.CreditCards.Remove(creditCard);
                    return 0;
                }
                catch(Exception ex)
                {
                    _logger.LogCritical(ex.ToString());
                    return 1;
                
            }          
        }

        public IEnumerable<CreditCard> GetAllCreditCards()
        {
            return _db.CreditCards.ToList();
        }

        public IEnumerable<CreditCard> GetAllCreditCardsByInstitutionId(int id)
        {
            return _db.CreditCards.Where(c => c.InstitutionId == id).ToList();
        }

        public IQueryable<CreditCard> GetBestBalanceTransferCardsByRate()
        {
            var cards = from c in _db.CreditCards where c.HasIntroBalanceTransferOffer select c;
            return cards.OrderBy(c => (c.IntroBalanceTransferRate + (c.HasIntroBalanceTransferFee ? c.IntroBalanceTransferFee : c.RegularBalanceTransferFee)) / (c.IntroBalanceTransferLengthInMonths / 12)).Take(50);
        }

        public IQueryable<CreditCard> GetLongestIntroBalanceTransferCards()
        {
            //var allCards = _db.CreditCards.Where(c => c.HasIntroBalanceTransferOffer == true && c.IntroBalanceTransferLengthInMonths > 12).ToList().OrderByDescending(c => c.IntroBalanceTransferLengthInMonths).Take(50);
            var allCards = from p in _db.CreditCards where p.HasIntroBalanceTransferOffer == true && p.IntroBalanceTransferLengthInMonths > 12 select p;
            return allCards.OrderByDescending(c => c.IntroBalanceTransferLengthInMonths).Take(50);

        }


        public CreditCard GetById(int id)
        {
            return _db.CreditCards.Find(id);
        }

        public int GetCountOfCreditCards()
        {
            return _db.CreditCards.Count();

        }

        public IEnumerable<CreditCard> GetCreditCardsByCardNetwork(CardNetwork cardNetwork)
        {
            return _db.CreditCards.Where(c => c.CardNetwork == cardNetwork).ToList();
        }

        public IQueryable<CreditCard> LowestRegularBalanceTransferCards()
        {
            //return _db.CreditCards.OrderBy(c => c.LowAPRBalanceTransfer).Take(50).ToList();
            var cards = from p in _db.CreditCards select p;
            return cards.OrderBy(c => c.LowAPRBalanceTransfer).Take(50);



        }

        public IQueryable<CreditCard> NoCashAdvanceFeeCards()
        {
            // return _db.CreditCards.Where(c => c.CashAdvanceFee == 0).OrderBy(c => c.LowAPRCashAdvance).ToList().Take(50);
            var cards = from p in _db.CreditCards where p.CashAdvanceFee == 0 select p;
            return cards.OrderBy(c => c.LowAPRCashAdvance).Take(50);

        }

        public IQueryable<CreditCard> NoBalanceTransferFeeCards()
        {
            // return _db.CreditCards.Where(c => c.RegularBalanceTransferFee == 0).OrderBy(c => c.LowAPRBalanceTransfer).ToList().Take(50);
            var cards = from p in _db.CreditCards where p.RegularBalanceTransferFee == 0 select p;
            return cards.OrderBy(c => c.LowAPRBalanceTransfer).Take(50);
        }

        public IQueryable<CreditCard> CashSignUpBonusCards()
        {
            //return _db.CreditCards.Where(c => c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.Cash).ToList().OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);

            var posts = from p in _db.CreditCards where p.HasSignUpBonus == true && p.SignUpBonusCategory == SignUpBonusCategory.Cash select p;
            return posts.OrderByDescending(p => p.SignUpBonusAmount / p.SignUpBonusSpendRequirement);
        }

        public IQueryable<CreditCard> AirlineSignUpBonusCards()
        {
            // return _db.CreditCards.Where(c => c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.AirLineMiles).ToList().OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
            var cards = from c in _db.CreditCards where c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.AirLineMiles select c;
            return cards.OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
        }

        public IQueryable<CreditCard> StorePointsBonusCards()
        {
            //  return _db.CreditCards.Where(c => c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.StorePoints).ToList().OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
            var cards = from c in _db.CreditCards where c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.StorePoints select c;
            return cards.OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
        }

        public IQueryable<CreditCard> HotelPointsBonusCards()
        {
            // return _db.CreditCards.Where(c => c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.HotelPoints).ToList().OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
            var cards = from c in _db.CreditCards where c.HasSignUpBonus && c.SignUpBonusCategory == SignUpBonusCategory.HotelPoints select c;
            return cards.OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
        }

        public IQueryable<CreditCard> CruisePointsBonusCards()
        {
            //   return _db.CreditCards.Where(c => c.HasSignUpBonus == true && c.SignUpBonusCategory == SignUpBonusCategory.CruisePoints).ToList().OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
            var cards = from c in _db.CreditCards where c.HasSignUpBonus && c.SignUpBonusCategory == SignUpBonusCategory.CruisePoints select c;
            return cards.OrderByDescending(c => c.SignUpBonusAmount / c.SignUpBonusSpendRequirement).Take(50);
        }

        public IQueryable<CreditCard> LowestCashAdvanceAPRCards()
        {
            // return _db.CreditCards.OrderBy(c => c.LowAPRCashAdvance).Take(50).ToList();
            var cards = from c in _db.CreditCards select c;
            return cards.OrderBy(c => c.LowAPRCashAdvance).Take(50);
        }

        public IEnumerable<CreditCard> GetAllCreditCardsWithNoInstitutionId()
        {
            return _db.CreditCards.Where(c => c.InstitutionId == null).ToList();
        }

    }
}
