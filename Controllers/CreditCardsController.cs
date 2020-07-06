using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardPortfolio.Data;
using CardPortfolio.Models;
using CardPortfolio.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using System.IO;
using CardPortfolio.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace CardPortfolio.Controllers
{
    public class CreditCardsController : Controller
    {


        private readonly ICreditCardData _creditCardData;
        private readonly IInstitutionData _institutionData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
     
        public CreditCardsController(ICreditCardData creditCardData, IInstitutionData institutionData,
            IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment)
        {
            _creditCardData = creditCardData;
            _institutionData = institutionData;
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
           

        }



         // GET: CreditCards/CashSignUpBonus
         public async Task<IActionResult> CashSignUpBonusCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Cash Sign Up Bonus";
            var cards = _creditCardData.CashSignUpBonusCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }



        // GET: CreditCards/AirlineBonusCards
        public async Task<IActionResult> AirlineBonusCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Airline Sign Up Bonus Cards";
            var cards = _creditCardData.AirlineSignUpBonusCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CreditCards/StoreBonusCards
        public async Task<IActionResult> StoreBonusCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Store Bonus Cards";
            var cards = _creditCardData.StorePointsBonusCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CreditCards/HotelPointsCards
        public async Task<IActionResult> HotelPointsCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Hotel Points Cards";
            var cards = _creditCardData.HotelPointsBonusCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CreditCards/CruisePointsCards
        public async Task<IActionResult> CruisePointsCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Cruise Points Cards";
            var cards = _creditCardData.CruisePointsBonusCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CreditCards/NoCashAdvanceFee
        public async Task<IActionResult> NoCashAdvanceFee(int? pageNumber)
        {
            ViewBag.PageHeader = "No Cash Adance Fee Cards";
            var cards = _creditCardData.NoCashAdvanceFeeCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
      
        // GET: CreditCards/NoBalanceTransferFeeCards
        public async Task<IActionResult> NoBalanceTransferFeeCards(int? pageNumber)
        {
            ViewBag.PageHeader = "No Balance Transfer Fee Cards";
            var cards = _creditCardData.NoBalanceTransferFeeCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: CreditCards/BalanceTransferCards
        public async Task<IActionResult> BalanceTransferCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Best Balance Transfer Cards";
            var cards = _creditCardData.GetBestBalanceTransferCardsByRate();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // GET: CreditCards/LowestRegularBalanceTransferAPR
        public async Task<IActionResult> LowestRegularBalanceTransferAPR(int? pageNumber)
        {
            ViewBag.PageHeader = "Lowest Regular Balance Transfer APR";
            var cards = _creditCardData.LowestRegularBalanceTransferCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));


        }

        // GET: CreditCards/LongestBalanceTransferOffer
        public async Task<IActionResult> LongestBalanceTransferOffer(int? pageNumber)
        {

            ViewBag.PageHeader = "Longest Balance Transfer Offers";
            var cards = _creditCardData.GetLongestIntroBalanceTransferCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> LowestCashAdvanceAprCards(int? pageNumber)
        {
            ViewBag.PageHeader = "Lowest Cash Advance APR Cards";
            var cards = _creditCardData.LowestCashAdvanceAPRCards();
            int pageSize = 10;
            
            return View(await PaginatedList<CreditCard>.CreateAsync(cards.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        //GET: CreditCards/Index
        public IActionResult Index()
        {
            var creditCardList = _creditCardData.GetAllCreditCards();
            
            return View(creditCardList);
        }

        // GET: CreditCards/AddImage/3
        [Authorize(Roles="Administrator")]
        public IActionResult AddImage(int id)
        {
            TempData["CardId"] = id;
            
            return View();
                
        }

        // POST: CreditCards/AddImage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddImageViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                string uniqueFileName = null;
                if(model.Image != null)
                {
                   string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                   uniqueFileName= Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                int cardId = (int)TempData["CardId"];
                var card = _creditCardData.GetById(cardId);
                if(card != null)
                {
                    card.ImagePath = uniqueFileName;
                    var commitStatus = _creditCardData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = (int)TempData["CardId"] });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
               
            }
            return RedirectToAction("Index");

        }

        
        // GET: CreditCards/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
                
            ViewBag.NetworkList = _htmlHelper.GetEnumSelectList<CardNetwork>();
            ViewBag.CategoryList = _htmlHelper.GetEnumSelectList<CardCategory>();
            ViewBag.SignUpBonusList = _htmlHelper.GetEnumSelectList<SignUpBonusCategory>();
            return View();
        }

        // POST: CreditCards/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(string name, bool hasIntroPurchaseOffer, double? introPurchaseRate,
            double? introPurchaseLengthInMonths, bool hasIntroBalanceTransferOffer,
            double? introBalanceTransferRate, double? introBalanceTransferLengthInMonths,
            double? lowAPRPurchases, double? highAPRPurchases, double lowAPRBalanceTransfer,
            double? highAPRBalanceTransfer, double? lowAPRCashAdvance, double? highAPRCashAdvance,
            bool hasSignUpBonus, double? signUpBonusSpendRequirement, double? signUpBonusAmount,
            bool hasIntroCashBackRate, double? introCashBackRate, double? introCashBackLengthInMonths,
            CardNetwork cardNetwork, string imagePath, string description, CardCategory cardCategory, 
            double? annualFee, bool hasIntroBalanceTransferFee, double? introBalanceTransferFee, double? cashAdvanceFee,
            double? regularBalanceTransferFee, bool isFixedRate, double? minimumCreditLine, double? maximumCreditLine, 
            bool hasCashBack, double? minimumPaymentPercent, decimal? minimumPaymentDollars, 
           double? foreignTransactionFee, SignUpBonusCategory signUpBonusCategory, string url, CreditCard creditCard
            )
        {
            if (ModelState.IsValid)
            {
                creditCard.Name = name;
                creditCard.HasIntroPurchaseOffer = hasIntroPurchaseOffer;
                creditCard.IntroPurchaseRate = introPurchaseRate;
                creditCard.IntroPurchaseLengthInMonths = introPurchaseLengthInMonths;
                creditCard.HasIntroBalanceTransferOffer = hasIntroBalanceTransferOffer;
                creditCard.IntroBalanceTransferRate = introBalanceTransferRate;
                creditCard.IntroBalanceTransferLengthInMonths = introBalanceTransferLengthInMonths;
                creditCard.LowAPRPurchases = lowAPRPurchases;
                creditCard.HighAPRPurchases = highAPRPurchases;
                creditCard.LowAPRBalanceTransfer = lowAPRBalanceTransfer;
                creditCard.HighAPRBalanceTransfer = highAPRBalanceTransfer;
                creditCard.LowAPRCashAdvance = lowAPRCashAdvance;
                creditCard.HighAPRCashAdvance = highAPRCashAdvance;
                creditCard.HasSignUpBonus = hasSignUpBonus;
                creditCard.SignUpBonusSpendRequirement = signUpBonusSpendRequirement;
                creditCard.SignUpBonusAmount = signUpBonusAmount;
                creditCard.SignUpBonusCategory = signUpBonusCategory;
                creditCard.CardNetwork = cardNetwork;
                creditCard.ImagePath = imagePath;
                creditCard.Description = description;
                creditCard.CardCategory = cardCategory;
                creditCard.AnnualFee = annualFee;
                creditCard.HasIntroBalanceTransferFee = hasIntroBalanceTransferFee;
                creditCard.IntroBalanceTransferFee = introBalanceTransferFee;
                creditCard.CashAdvanceFee = cashAdvanceFee;
                creditCard.RegularBalanceTransferFee = regularBalanceTransferFee;
                creditCard.IsFixedRate = isFixedRate;
                creditCard.MinimumCreditLine = minimumCreditLine;
                creditCard.MaximumCreditLine = maximumCreditLine;
                creditCard.HasCashBack = hasCashBack;
                creditCard.MinimumPaymentPercent = minimumPaymentPercent;
                creditCard.MinimumPaymentInDollars = minimumPaymentDollars;
                creditCard.ForeignTransactionFee = foreignTransactionFee;
                creditCard.Url = url;

                var addStatus = _creditCardData.Add(creditCard);
                if(addStatus == 0)
                {
                    var commitStatus = _creditCardData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = creditCard.Id });
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                           
            }
            return RedirectToAction("Index");
        }
        
        // GET: CreditCards/Delete/3
        [Authorize(Roles= "Administrator")]
        public IActionResult Delete(int id)
        {
            
            TempData["CardId"] = id;
            
            return View();
        }

        // POST: CreditCards/Delete/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete()
        {
            int id = (int)TempData["CardId"];
            // Refactor
            var card = _creditCardData.GetById(id);
            if (card != null)
            {
                var removeStatus = _creditCardData.Delete(card);
                if(removeStatus == 0)
                {
                    var commitStatus = _creditCardData.Commit();
                    if(commitStatus == 0)
                    {
                        // Add Success Message here
                        return RedirectToAction("Index");
                    }

                    // Add Failed to Save message here
                    
                }
                else
                {
                    // add failed to delete message here
                }
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: CreditCards/Details/5
        public IActionResult Details(int id)
        {
           
            var card = _creditCardData.GetById(id);
            if(card != null)
            {
                
                return View(card);

            }
            else
            {
                // replace with 404 page
                return RedirectToAction("Index");
            }
        }

        // GET: CreditCards/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            // Refactor
            var card = _creditCardData.GetById(id);
            if (card != null)
            {
                TempData["CardId"] = id;
                ViewBag.NetworkList = _htmlHelper.GetEnumSelectList<CardNetwork>();
                ViewBag.CategoryList = _htmlHelper.GetEnumSelectList<CardCategory>();
                ViewBag.SignUpBonusList = _htmlHelper.GetEnumSelectList<SignUpBonusCategory>();
                return View(card);
            }
            // Replace with Error Page
            return RedirectToAction("Index");
        }

        // POST: CreditCards/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string name, bool hasIntroPurchaseOffer, double? introPurchaseRate,
            double? introPurchaseLengthInMonths, bool hasIntroBalanceTransferOffer,
            double? introBalanceTransferRate, double? introBalanceTransferLengthInMonths,
            double lowAPRPurchases, double highAPRPurchases, double lowAPRBalanceTransfer,
            double highAPRBalanceTransfer, double lowAPRCashAdvance, double highAPRCashAdvance,
            bool hasSignUpBonus, double? signUpBonusSpendRequirement, double? signUpBonusAmount,
            bool hasIntroCashBackRate, double? introCashBackRate, double? introCashBackLengthInMonths,
            CardNetwork cardNetwork, string imagePath, string description, CardCategory cardCategory,
            double? annualFee, bool hasIntroBalanceTransferFee, double? introBalanceTransferFee, double? cashAdvanceFee,
            double? regularBalanceTransferFee, bool isFixedRate, double? minimumCreditLine, double? maximumCreditLine,
            bool hasCashBack, double? minimumPaymentPercent, decimal? minimumPaymentInDollars, SignUpBonusCategory signUpBonusCategory,  
            double? foreignTransactionFee, string url)
        {

            if (ModelState.IsValid)
            {
                int id = (int)TempData["CardId"];
                var card = _creditCardData.GetById(id);
                card.Name = name;
                card.HasIntroPurchaseOffer = hasIntroPurchaseOffer;
                card.IntroPurchaseRate = introPurchaseRate;
                card.IntroPurchaseLengthInMonths = introPurchaseLengthInMonths;
                card.HasIntroBalanceTransferOffer = hasIntroBalanceTransferOffer;
                card.IntroBalanceTransferRate = introBalanceTransferRate;
                card.IntroBalanceTransferLengthInMonths = introBalanceTransferLengthInMonths;
                card.LowAPRPurchases = lowAPRPurchases;
                card.HighAPRPurchases = highAPRPurchases;
                card.LowAPRBalanceTransfer = lowAPRBalanceTransfer;
                card.HighAPRBalanceTransfer = highAPRBalanceTransfer;
                card.LowAPRCashAdvance = lowAPRCashAdvance;
                card.HighAPRCashAdvance = highAPRCashAdvance;
                card.HasSignUpBonus = hasSignUpBonus;
                card.SignUpBonusSpendRequirement = signUpBonusSpendRequirement;
                card.SignUpBonusAmount = signUpBonusAmount;              
                card.CardNetwork = cardNetwork;
                card.ImagePath = imagePath;
                card.Description = description;
                card.CardCategory = cardCategory;
                card.AnnualFee = annualFee;
                card.SignUpBonusCategory = signUpBonusCategory;
                card.HasIntroBalanceTransferFee = hasIntroBalanceTransferFee;
                card.IntroBalanceTransferFee = introBalanceTransferFee;
                card.CashAdvanceFee = cashAdvanceFee;
                card.RegularBalanceTransferFee = regularBalanceTransferFee;
                card.IsFixedRate = isFixedRate;
                card.MinimumCreditLine = minimumCreditLine;
                card.MaximumCreditLine = maximumCreditLine;
                card.HasCashBack = hasCashBack;
                card.MinimumPaymentPercent = minimumPaymentPercent;
                card.MinimumPaymentInDollars = minimumPaymentInDollars;
                card.ForeignTransactionFee = foreignTransactionFee;
                card.UpdatedDate = DateTime.Now;
                card.Url = url;


                var commitStatus = _creditCardData.Commit();
                if(commitStatus == 0)
                {
                    // add success message to be displayed here
                    return RedirectToAction("Details", new { id = card.Id });
                }
                else
                {
                    // add failed to save changes method here 
                }             

            }

            return RedirectToAction("Index");
        }
        

        // GET: CreditCards/InstitutionCards/5
        public IActionResult InstitutionCards(int id)
        {
            var institutionCards = _creditCardData.GetAllCreditCardsByInstitutionId(id);         
            return View(institutionCards);
        }
      
        // GET: CreditCards/RemoveCard/5
        [Authorize(Roles ="Administrator")]
        public IActionResult RemoveCard(int id)
        {
            var card = _creditCardData.GetById(id);
            if(card != null)
            {
                TempData["CardId"] = id;
                ViewBag.Title = card.Name;
                return View(card);
            }
            else
            {
               
                return RedirectToAction("Index");

            }
        }

        
        // POST: CreditCards/RemoveCard/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult RemoveCard()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["CardId"];
                var card = _creditCardData.GetById(id);

                if (card != null)
                {
                    var institutionId = card.InstitutionId;
                    card.InstitutionId = null;
                    var commitStatus = _creditCardData.Commit();
                    if (commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", "Institutions", new { id = institutionId });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // card is null
                    return RedirectToAction("Index");
                }

            }
            // if model state is not valid
            return View();
        }

    }
}