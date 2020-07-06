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
    // TODO: Figure out what to do here
    [Authorize(Roles ="Administrator")]
    public class InstitutionsController : Controller
    {

        
        private readonly ICreditCardData _creditCardData;
        private readonly IInstitutionData _institutionData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public InstitutionsController(ICreditCardData creditCardData, IInstitutionData institutionData,
            IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment)
        {
            
            _creditCardData = creditCardData;
            _institutionData = institutionData;
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Institutions/Index
        public IActionResult Index()
        {
            
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