using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CardPortfolio.Data;
using CardPortfolio.Models;
using CardPortfolio.Models.Enums;
using CardPortfolio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardPortfolio.Controllers
{
    public class InstitutionsController : Controller
    {
        private readonly IAutoLoanData _autoLoanData;
        private readonly ICreditCardData _creditCardData;
        private readonly IInstitutionData _institutionData;
        private readonly ICertificateAccountData _certificateAccountData;
        private readonly ICheckingAccountData _checkingAccountData;
        private readonly IHomeEquityLineOfCreditData _homeEquityLineOfCreditData;
        private readonly IHomeEquityLoanData _homeEquityLoanData;
        private readonly IMoneyMarketAccountData _moneyMarketAccountData;
        private readonly IMortgageData _mortgageData;
        private readonly ISavingsAccountData _savingsAccountData;
        private readonly ISecuredLineOfCreditData _securedLineOfCreditData;
        private readonly ISecuredPersonalLoanData _securedPersonalLoanData;
        private readonly IUnsecuredLineOfCreditData _unsecuredLineOfCreditData;
        private readonly IUnsecuredPersonalLoanData _unsecuredPersonalLoanData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public InstitutionsController(ICreditCardData creditCardData, IInstitutionData institutionData,
            IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment, ICertificateAccountData certificateAccountData,
            ICheckingAccountData checkingAccountData, IMoneyMarketAccountData moneyMarketAccountData,
            ISavingsAccountData savingsAccountData, IAutoLoanData autoLoanData,
            IHomeEquityLineOfCreditData homeEquityLineOfCreditData, IHomeEquityLoanData homeEquityLoanData,
            IMortgageData mortgageData, ISecuredLineOfCreditData securedLineOfCreditData,
            ISecuredPersonalLoanData securedPersonalLoanData, IUnsecuredLineOfCreditData unsecuredLineOfCreditData, IUnsecuredPersonalLoanData unsecuredPersonalLoanData)
        {
            _autoLoanData = autoLoanData;
            _creditCardData = creditCardData;
            _institutionData = institutionData;
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
            _certificateAccountData = certificateAccountData;
            _checkingAccountData = checkingAccountData;
            _moneyMarketAccountData = moneyMarketAccountData;
            _savingsAccountData = savingsAccountData;
            _homeEquityLineOfCreditData = homeEquityLineOfCreditData;
            _homeEquityLoanData = homeEquityLoanData;
            _mortgageData = mortgageData;
            _securedLineOfCreditData = securedLineOfCreditData;
            _securedPersonalLoanData = securedPersonalLoanData;
            _unsecuredLineOfCreditData = unsecuredLineOfCreditData;
            _unsecuredPersonalLoanData = unsecuredPersonalLoanData;

        }

        // GET: Institutions/Index
        public IActionResult Index()
        {
            // Refactor
            var institutionList = _institutionData.GetAll();

            return View(institutionList);
        }

        // GET: Institutions/AddCard/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddCard(int id)
        {
            TempData["InstitutionId"] = id;
            var creditCardList = _creditCardData.GetAllCreditCardsWithNoInstitutionId();
            ViewBag.CreditCardId = new SelectList(creditCardList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddCard/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddCard(int creditCardId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var creditCard = _creditCardData.GetById(creditCardId);
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);

                if (creditCard != null && institution != null)
                {
                    creditCard.InstitutionId = institutionId;
                    institution.CreditCards.Add(creditCard);
                    
                    _institutionData.Commit();
                }
                return RedirectToAction("Details", new { id = institutionId });
            } 


            return RedirectToAction("Index");
       
        }

        //GET: Institutions/AddCertificateAccount/5
        [Authorize(Roles ="Administrator")]
        public IActionResult AddCertificateAccount(int id)
        {
            TempData["InstitutionId"] = id;
            var certificateAccountList = _certificateAccountData.GetUnassignedCertificateAccounts();
            ViewBag.CertificateAccountId = new SelectList(certificateAccountList, "Id", "Name");
            return View();
        }

        //POST: Institutions/AddCertificateAccount/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult AddCertificateAccount(int certificateAccountId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var account = _certificateAccountData.GetById(certificateAccountId);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if(account != null && institution != null)
                {
                    account.InstitutionId = institutionId;
                    account.ImageUrl = institution.ImagePath;
                    var commitStatus = _certificateAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = institutionId });
                    }
                    else
                    {
                        // failure to save message
                    }
                    
                }
                
            }
            return RedirectToAction("Index");

        }

        // GET: Institutions/AddCheckingAccount/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddCheckingAccount(int id)
        {
            TempData["InstitutionId"] = id;
            var checkingAccountList = _checkingAccountData.GetUnassignedCheckingAccounts();
            ViewBag.CheckingAccountId = new SelectList(checkingAccountList, "Id", "Name");
            return View();
        }


        // POST: Institutions/AddCheckingAccount/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddCheckingAccount(int checkingAccountId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var account = _checkingAccountData.GetById(checkingAccountId);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (account != null && institution != null)
                {
                    account.InstitutionId = institutionId;
                    account.ImageUrl = institution.ImagePath;
                    var commitMessage = _institutionData.Commit();
                    if(commitMessage == 0)
                    {
                        return RedirectToAction("Details", new { id = institutionId });
                    }
                    else
                    {
                        // Failed to save
                        return RedirectToAction("Index");
                    }

                    
                }

            }
            return RedirectToAction("Index");

        }

        // GET: Institutions/AddMoneyMarketAccount/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddMoneyMarketAccount(int id)
        {
            TempData["InstitutionId"] = id;
            var moneyMarketAccountList = _moneyMarketAccountData.GetUnassignedMoneyMarketAccounts(); 
            ViewBag.MoneyMarketAccountId = new SelectList(moneyMarketAccountList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddMoneyMarketAccount/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddMoneyMarketAccount(int moneyMarketAccountId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var account = _moneyMarketAccountData.GetById(moneyMarketAccountId);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (account != null && institution != null)
                {
                    account.InstitutionId = institutionId;
                    account.ImageUrl = institution.ImagePath;
                    var commitStatus = _moneyMarketAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        // everything went right
                        return RedirectToAction("Details", new { id = institutionId });
                    }
                    else
                    {
                        // error message cannot save changes
                        return RedirectToAction("Index");
                    }
                    
                }

            }
            return RedirectToAction("Index");

        }

        // GET: Institutions/AddSavingsAccount/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddSavingsAccount(int id)
        {
            TempData["InstitutionId"] = id;
            var savingsAccountList = _savingsAccountData.GetUnassignedSavingsAccounts();
            ViewBag.SavingsAccountId = new SelectList(savingsAccountList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddSavingsAccount/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddSavingsAccount(int savingsAccountId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var account = _savingsAccountData.GetById(savingsAccountId);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (account != null && institution != null)
                {
                    account.InstitutionId = institutionId;
                    account.ImageUrl = institution.ImagePath;
                    var commitStatus = _savingsAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit successful
                        return RedirectToAction("Details", new { id = institutionId });

                    }
                    else
                    {
                        // save unsuccessful return to index with TempData Error Message
                        return RedirectToAction("Index");
                    }
                    
                }

            }
            return RedirectToAction("Index");

        }

        // GET: Institutions/AddAutoLoan/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddAutoLoan(int id)
        {
            TempData["InstitutionId"] = id;
            var autoLoanList = _autoLoanData.GetUnassignedAutoLoans();
            ViewBag.AutoLoanId = new SelectList(autoLoanList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddAutoLoan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddAutoLoan(int autoLoanId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var autoLoan = _autoLoanData.GetAutoLoanById(autoLoanId);
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);

                if (autoLoan != null && institution != null)
                {
                    autoLoan.InstitutionId = institutionId;
                    var commitStatus = _autoLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit successful
                        return RedirectToAction("Details", new { id = institutionId });

                    }
                    else
                    {
                        // commit unsucessful, add failure to add loan message
                        return RedirectToAction("Index");
                    }
                    
                }
                
            }


            return RedirectToAction("Index");
        }


        // GET: Institutions/AddHomeEquityLineOfCredit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddHomeEquityLineOfCredit(int id)
        {
            TempData["InstitutionId"] = id;
            var helocList = _homeEquityLineOfCreditData.GetUnassignedHomeEquityLinesOfCredit();
            ViewBag.Id = new SelectList(helocList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddHomeEquityLineOfCredit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddHomeEquityLineOfCredit(int Id, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var homeEquityLoc = _homeEquityLineOfCreditData.GetById(Id);
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);

                if(homeEquityLoc != null && institution != null)
                {
                    homeEquityLoc.InstitutionId = institutionId;
                    var commitStatus = _homeEquityLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = institutionId });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                    
                }
                

            }
            return RedirectToAction("Index");
        }


        // GET: Institutions/AddHomeEquityLoan/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddHomeEquityLoan(int id)
        {
            TempData["InstitutionId"] = id;
            var homeEquityLoanList = _homeEquityLoanData.GetUnassignedHomeEquityLoans();
            ViewBag.HomeEquityLoanId = new SelectList(homeEquityLoanList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddHomeEquityLoan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddHomeEquityLoan(int homeEquityLoanId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var homeEquityLoan = _homeEquityLoanData.GetById(homeEquityLoanId);
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);

                if(homeEquityLoan != null && institution != null)
                {
                    homeEquityLoan.InstitutionId = institutionId;
                    var commitStatus = _homeEquityLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = institution.Id });

                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }

                }
            }
            return RedirectToAction("Index");
        }

        // GET: Institutions/AddMortgage/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddMortgage(int id)
        {
            TempData["InstitutionId"] = id;
            var mortgageList = _mortgageData.GetUnassignedMortgages();
            ViewBag.MortgageId = new SelectList(mortgageList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddMortgage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddMortgage(int mortgageId, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var mortgage = _mortgageData.GetById(mortgageId);
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if(mortgage != null && institution != null)
                {
                    mortgage.InstitutionId = institutionId;
                    var commitStatus = _mortgageData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = institution.Id });

                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
                

            }
            // Replace with something went wrong page
            return RedirectToAction("Index");
        }

        // GET: Institutions/AddSecuredLoc/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddSecuredLoc(int id)
        {
            TempData["InstitutionId"] = id;
            var securedLocList = _securedLineOfCreditData.GetUnassignedSecuredLinesOfCredit();
            ViewBag.Id = new SelectList(securedLocList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddSecuredLoc/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddSecuredLoc(int Id, string filler=null)
        {
            if (ModelState.IsValid)
            {
                var loc = _securedLineOfCreditData.GetById(Id);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if(loc!= null && institution != null)
                {
                    loc.InstitutionId = institution.Id;
                    var commitStatus = _securedLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = institution.Id });

                    }
                    else
                    {
                        // commit failed
                        
                        return RedirectToAction("Index");

                    }
                    
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Institutions/AddSecuredLoan/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddSecuredLoan(int id)
        {
            TempData["InstitutionId"] = id;
            var securedLoanList = _securedPersonalLoanData.GetUnassignedPersonalLoans();
            ViewBag.Id = new SelectList(securedLoanList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddSecuredLoan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddSecuredLoan(int Id, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var loan = _securedPersonalLoanData.GetById(Id);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (loan != null && institution != null)
                {
                    loan.InstitutionId = institution.Id;
                    var commitStatus = _securedPersonalLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = institution.Id });

                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Institutions/AddUnsecuredLineOfCredit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddUnsecuredLineOfCredit(int id)
        {
            TempData["InstitutionId"] = id;
            var unsecuredLocList = _unsecuredLineOfCreditData.GetUnassignedUnsecuredLinesOfCredit();
            ViewBag.Id = new SelectList(unsecuredLocList, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddUnsecuredLineOfCredit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddUnsecuredLineOfCredit(int Id, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var loc = _unsecuredLineOfCreditData.GetById(Id);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (loc != null && institution != null)
                {
                    loc.InstitutionId = institution.Id;
                    var commitStatus = _unsecuredLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = institution.Id });

                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Institutions/AddUnsecuredLoan/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddUnsecuredLoan(int id)
        {
            TempData["InstitutionId"] = id;
            var list = _unsecuredPersonalLoanData.GetUnassignedPersonalLoans();
            ViewBag.Id = new SelectList(list, "Id", "Name");
            return View();
        }

        // POST: Institutions/AddUnsecuredLoan/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddUnsecuredLoan(int Id, string filler = null)
        {
            if (ModelState.IsValid)
            {
                var loan = _unsecuredPersonalLoanData.GetById(Id);
                var institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (loan != null && institution != null)
                {
                    loan.InstitutionId = institution.Id;
                    var commitStatus = _unsecuredPersonalLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded 
                        return RedirectToAction("Details", new { id = institution.Id });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
            }
            return RedirectToAction("Index");
        }


        // GET: Institutions/AddImage/5
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(int id)
        {
            TempData["InstitutionId"] = id;

            return View();

        }

        // POST: Institutions/AddImage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddInstitutionImageViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.ImagePath != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImagePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.ImagePath.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                if (institution != null)
                {
                    institution.ImagePath = uniqueFileName;
                    var commitStatus = _institutionData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = (int)TempData["InstitutionId"] });

                    }
                    else
                    {
                        // failed
                        return RedirectToAction("Index");
                    }
                }
            }
            // Model state not valid
            return RedirectToAction("Index");

        }

        // GET: Institutions/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.TypeList = _htmlHelper.GetEnumSelectList<InstitutionType>();
            ViewBag.CreditBureauList = _htmlHelper.GetEnumSelectList<CreditBureau>();
            ViewBag.RestrictionList = _htmlHelper.GetEnumSelectList<RestrictionType>();
            return View();

        }

        // POST: Institutions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(string name, InstitutionType institutionType, string imagePath, string description, bool allowsBalanceTransferCash, bool hasRestrictionsToJoin, RestrictionType restrictionType, double? lengthBetweenCreditLineIncreases,
            CreditBureau creditBureau, double? scoreforLowestAPR, bool hasPoints, double? pointsCashValue, Institution institution)
        {
            if (ModelState.IsValid)
            {
                institution.Name = name;
                institution.InstitutionType = institutionType;
                institution.ImagePath = imagePath;
                institution.Description = description;
                institution.AllowsBalanceTransferCash = allowsBalanceTransferCash;
                institution.HasRestrictionsToJoin = hasRestrictionsToJoin;
                institution.RestrictionType = restrictionType;
                institution.LengthBetweenCreditLineIncreases = lengthBetweenCreditLineIncreases;
                institution.CreditBureau = creditBureau;
                institution.ScoreForLowestAPR = scoreforLowestAPR;

                var createStatus = _institutionData.Create(institution);
                if (createStatus == 0)
                {
                    // create succeeded
                    var commitStatus = _institutionData.Commit();
                    if (commitStatus == 0)
                    {
                        //. commit succeeded
                        return RedirectToAction("Details", new { id = institution.Id });

                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }

                }
                else
                {
                    // create failed
                    return RedirectToAction("Index");
                }
            }
            // modelstate not valid
            return RedirectToAction("Index");
           
        }

        // GET: Institutions/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            var institution = _institutionData.GetById(id);
            if(institution == null)
            {
                // Replace with not found page
                return RedirectToAction("Index");
            }
            TempData["InstitutionId"] = id;
            return View(institution);
        }

        // POST: Institutions/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                var deleteStatus = _institutionData.Delete(institution);
                if (deleteStatus == 0)
                {
                    // delete succeeded
                    var commitStatus = _institutionData.Commit();
                    if (commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    // delete failed
                    return RedirectToAction("Index");

                }

            }
            // model state not valid
            return RedirectToAction("Index");
        }

        // GET: Institutions/Details/5
        public IActionResult Details(int id)
        {



            var institution = _institutionData.GetById(id);
            if (institution != null)
            {
                ViewBag.CardList = _institutionData.GetInstitutionsCreditCards(institution.Id);
                ViewBag.UnsecuredLoanList = _institutionData.GetInstitutionsUnsecuredPersonalLoans(institution.Id);
                ViewBag.UnsecuredLocList = _institutionData.GetInstitutionsUnsecuredLinesOfCredit(institution.Id);
                ViewBag.SecuredLoanList = _institutionData.GetInstitutionsSecuredPersonalLoans(institution.Id);
                ViewBag.SecuredLocList = _institutionData.GetInstitutionsSecuredLinesOfCredit(institution.Id);
                ViewBag.AutoLoanList = _institutionData.GetInstitutionsAutoLoans(institution.Id);
                ViewBag.MortgageList = _institutionData.GetInstitutionsMortgages(institution.Id);
                ViewBag.SavingsAccountList = _institutionData.GetInstitutionsSavingsAccounts(institution.Id);
                ViewBag.CertificateAccountList = _institutionData.GetInstitutionsCds(institution.Id);
                ViewBag.CheckingAccountList = _institutionData.GetInstitutionsCheckingAccounts(institution.Id);
                ViewBag.MoneyMarketAccountList = _institutionData.GetInstitutionsMoneyMarketAccount(institution.Id);
                ViewBag.HELOCList = _institutionData.GetInstitutionHomeEquityLinesOfCredit(institution.Id);
                ViewBag.HELOANList = _institutionData.GetInstitutionsHomeEquityLoans(institution.Id);
            }

            return View(institution);
        }

        // GET: Institutions/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            var institution = _institutionData.GetById(id);
            if(institution == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.TypeList = _htmlHelper.GetEnumSelectList<InstitutionType>();
            ViewBag.CreditBureauList = _htmlHelper.GetEnumSelectList<CreditBureau>();
            ViewBag.RestrictionList = _htmlHelper.GetEnumSelectList<RestrictionType>();
            TempData["InstitutionId"] = id;
            return View(institution);
        }

        // POST: Institutions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(string name, string description, string imagePath, InstitutionType institutionType, bool allowsBalanceTransferCash, bool hasRestrictionsToJoin, RestrictionType restrictionType, double? lengthBetweenCreditLineIncreases, CreditBureau creditBureau, double? scoresForLowestAPR, bool hasPoints, double? pointsCashValue)
        {
            if (ModelState.IsValid)
            {
                int institutionId = (int)TempData["InstitutionId"];
                var institution = _institutionData.GetById(institutionId);
                institution.Name = name;
                institution.Description = description;
                institution.ImagePath = imagePath;
                institution.InstitutionType = institutionType;
                institution.AllowsBalanceTransferCash = allowsBalanceTransferCash;
                institution.HasRestrictionsToJoin = hasRestrictionsToJoin;
                institution.RestrictionType = restrictionType;
                institution.LengthBetweenCreditLineIncreases = lengthBetweenCreditLineIncreases;
                institution.CreditBureau = creditBureau;
                institution.ScoreForLowestAPR = scoresForLowestAPR;
                institution.UpdatedDate = DateTime.Now;

               var commitStatus = _institutionData.Commit();
                if(commitStatus == 0)
                {
                    // commit succeeded
                    return RedirectToAction("Details", new { id = institution.Id });

                }
                else
                {
                    // commit failed
                    return RedirectToAction("Index");
                }
            }
            // ModelState not validS
            return RedirectToAction("Index");
        }


        // GET: Institutions/GetAllBanks
        public IActionResult GetAllBanks()
        {
            var bankList = _institutionData.GetAllBanks();
            return View(bankList);
        }

        // GET: Institutions/GetAllCreditUnions
        public IActionResult GetAllCreditUnions()
        {
            var creditUnionList = _institutionData.GetAllCreditUnions();
            return View(creditUnionList);
        }

        // GET: Institutions/CreditUnionsAnyoneCanJoin
        public IActionResult CreditUnionsAnyoneCanJoin()
        {
            var creditUnions = _institutionData.CreditUnionsAnyoneCanJoin();
            return View(creditUnions);
        }


    }
}