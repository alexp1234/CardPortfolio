using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CardPortfolio.Data;
using CardPortfolio.Models;
using CardPortfolio.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardPortfolio.Controllers
{
    public class HomeEquityLinesOfCreditController : Controller
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHomeEquityLineOfCreditData _homeEquityLineOfCreditData;
        public HomeEquityLinesOfCreditController(IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment, IHomeEquityLineOfCreditData homeEquityLineOfCreditData)
        {
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
            _homeEquityLineOfCreditData = homeEquityLineOfCreditData;

        }
        public IActionResult Index()
        {
            // TODO: Refactor
            var helocList = _homeEquityLineOfCreditData.GetAllHomeEquityLinesOfCredit();
            return View(helocList);
        }

        // GET: HomeEquityLinesOfCredit/AddImage/3
        [Authorize(Roles= "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["HomeEquityLineOfCreditId"] = id;
            return View();

        }

        // POST: HomeEquityLinesOfCredit/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddHomeEquityLineOfCreditImageViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                if (model.Image != null)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                int helocId = (int)TempData["HomeEquityLineOfCreditId"];
                // TODO: Refactor
                var loan = _homeEquityLineOfCreditData.GetById(helocId);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;
                    var commitStatus = _homeEquityLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // replace with error page
                        return RedirectToAction("Index");
                    }
                }
                
            }
            return RedirectToAction("Index");
        }

        // GET: HomeEquityLinesOfCredit/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {            
            return View();
        }

        // POST: HomeEquityLinesOfCredit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minimumTermInMonths, int? maximumTermInMonths,
            bool hasOriginationFee, double? originationFee, bool hasAnnualFee, double? annualFee,
            bool hasAdvanceFee, double? advanceFee, string name, bool arePaymentsInterestOnly, 
            double? minimumPayment, double? Ltv, string linkURL, HomeEquityLineOfCredit homeEquityLoc )
        {
            if (ModelState.IsValid)
            {
                homeEquityLoc.LowApr = lowApr;
                homeEquityLoc.HighApr = highApr;
                homeEquityLoc.MinimumAmount = minimumAmount;
                homeEquityLoc.MaximumAmount = maximumAmount;
                homeEquityLoc.MinimumTermInMonths = minimumTermInMonths;
                homeEquityLoc.MaximumTermInMonths = maximumTermInMonths;
                homeEquityLoc.HasOriginationFee = hasOriginationFee;
                homeEquityLoc.OriginationFee = originationFee;
                homeEquityLoc.HasAnnualFee = hasAnnualFee;
                homeEquityLoc.AnnualFee = annualFee;
                homeEquityLoc.HasAdvanceFee = hasAdvanceFee;
                homeEquityLoc.AdvanceFee = advanceFee;
                homeEquityLoc.Name = name;
                homeEquityLoc.ArePaymentsInterestOnly = arePaymentsInterestOnly;
                homeEquityLoc.MinimumPayment = minimumPayment;
                homeEquityLoc.LTV = Ltv;
                homeEquityLoc.LinkURL = linkURL;
               var addStatus = _homeEquityLineOfCreditData.AddHomeEquityLineOfCredit(homeEquityLoc);
                if(addStatus == 0)
                {
                    var commitStatus = _homeEquityLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        // add error saving page 
                    }
                }
                else
                {
                    // add failure to add page
                }
                


            }
            return RedirectToAction("Index");
        }

        // GET: HomeEquityLinesOfCredit/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            TempData["HomeEquityLineOfCreditId"] = id;
            return View();
        }

        // POST: HomeEquityLinesOfCredit/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["HomeEquityLineOfCreditId"];
                var homeEquityLoc = _homeEquityLineOfCreditData.GetById(id);
                if (homeEquityLoc != null)
                {
                    // TODO: Refactor
                    var removeStatus = _homeEquityLineOfCreditData.RemoveHomeEquityLineOfCredit(homeEquityLoc);
                    if (removeStatus == 0)
                    {
                        var commitStatus = _homeEquityLineOfCreditData.Commit();
                        if (commitStatus == 0)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            // add failure to save changes error
                        }
                    }
                    else
                    {
                        // return failure 
                    }

                }
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: HomeEquityLinesOfCredit/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            // TODO: Refactor
            var homeEquityLoc = _homeEquityLineOfCreditData.GetById(id);
            if (homeEquityLoc != null)
            {
                TempData["HomeEquityLineOfCreditId"] = id;               
                return View(homeEquityLoc);

            }
            // Replace With 404 Page
            return RedirectToAction("Index");
        }

        // POST: HomeEquityLinesOfCredit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minimumTermInMonths, int? maximumTermInMonths, 
            bool hasOriginationFee, double? originationFee, bool hasAnnualFee, double? annualFee, 
            bool hasAdvanceFee, double? advanceFee, string name, bool arePaymentsInterestOnly,
            double? minimumPayment, double? Ltv, string linkURL)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["HomeEquityLineOfCreditId"];
                var homeEquityLoc = _homeEquityLineOfCreditData.GetById(id);
                if (homeEquityLoc != null)
                {
                    homeEquityLoc.LowApr = lowApr;
                    homeEquityLoc.HighApr = highApr;
                    homeEquityLoc.MinimumAmount = minimumAmount;
                    homeEquityLoc.MaximumAmount = maximumAmount;
                    homeEquityLoc.MinimumTermInMonths = minimumTermInMonths;
                    homeEquityLoc.MaximumTermInMonths = maximumTermInMonths;
                    homeEquityLoc.HasOriginationFee = hasOriginationFee;
                    homeEquityLoc.OriginationFee = originationFee;
                    homeEquityLoc.HasAnnualFee = hasAnnualFee;
                    homeEquityLoc.AnnualFee = annualFee;
                    homeEquityLoc.HasAdvanceFee = hasAdvanceFee;
                    homeEquityLoc.AdvanceFee = advanceFee;
                    homeEquityLoc.Name = name;
                    homeEquityLoc.ArePaymentsInterestOnly = arePaymentsInterestOnly;
                    homeEquityLoc.MinimumPayment = minimumPayment;
                    homeEquityLoc.LTV = Ltv;
                    homeEquityLoc.LinkURL = linkURL;
                    var commitStatus = _homeEquityLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // give error saving changes page
                    }                   
                    
                }

            }
            return RedirectToAction("Index");
        }
    }
}