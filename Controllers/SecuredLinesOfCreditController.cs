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
    public class SecuredLinesOfCreditController : Controller
    {
        private readonly ISecuredLineOfCreditData _securedLineOfCreditData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public SecuredLinesOfCreditController( IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment, ISecuredLineOfCreditData securedLineOfCreditData)
        {
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
            _securedLineOfCreditData = securedLineOfCreditData;

        }
        public IActionResult Index()
        {
            var selocList = _securedLineOfCreditData.GetAllSecuredLinesOfCredit();
            return View(selocList);
        }

        // GET: HomeEquityLinesOfCredit/AddImage/3
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["Id"] = id;
            return View();

        }

        // POST: HomeEquityLinesOfCredit/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddSecuredLineOfCreditImageViewModel model)
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
                int loc = (int)TempData["Id"];
                var loan = _securedLineOfCreditData.GetById(loc);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;
                    var commitStatus = _securedLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = (int)TempData["Id"] });

                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // loan is null
                    return RedirectToAction("Index");
                }
            }
            // model state not valid
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
        public IActionResult Create(double? collateralToLocRatio, double? lowApr,
            double? highApr, double? minimumAmount, double? maximumAmount, 
            int? minimumTermInMonths, int? maximumTermInMonths, bool hasOriginationFee,
            double? originationFee, bool hasAnnualFee, double? annualFee, bool hasAdvanceFee,
            double? advanceFee, string name, bool arePaymentsInterestOnly, double? minimumPayment,
            string linkURL, SecuredLineOfCredit securedLineOfCredit )
        {
            if (ModelState.IsValid)
            {

                securedLineOfCredit.CollateralToLocRatio = collateralToLocRatio;
                securedLineOfCredit.LowApr = lowApr;
                securedLineOfCredit.HighApr = highApr;
                securedLineOfCredit.MinimumAmount = minimumAmount;
                securedLineOfCredit.MaximumAmount = maximumAmount;
                securedLineOfCredit.MinimumTermInMonths = minimumTermInMonths;
                securedLineOfCredit.MaximumTermInMonths = maximumTermInMonths;
                securedLineOfCredit.HasOriginationFee = hasOriginationFee;
                securedLineOfCredit.OriginationFee = originationFee;
                securedLineOfCredit.HasAnnualFee = hasAnnualFee;
                securedLineOfCredit.AnnualFee = annualFee;
                securedLineOfCredit.HasAdvanceFee = hasAdvanceFee;
                securedLineOfCredit.AdvanceFee = advanceFee;
                securedLineOfCredit.Name = name;
                securedLineOfCredit.ArePaymentsInterestOnly = arePaymentsInterestOnly;
                securedLineOfCredit.MinimumPayment = minimumPayment;
                securedLineOfCredit.LinkURL = linkURL;
                var addStatus = _securedLineOfCreditData.AddSecuredLineOfCredit(securedLineOfCredit);
                if(addStatus == 0)
                {
                    // add succeeded
                    var commitStatus = _securedLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = securedLineOfCredit.Id });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // add failed
                    return RedirectToAction("Index");
                }
                
               


            }
            // model state not valid
            return RedirectToAction("Index");
        }

        // GET: HomeEquityLinesOfCredit/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            TempData["SecuredLineOfCreditId"] = id;
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
                int id = (int)TempData["SecuredLineOfCreditId"];
                var securedLineOfCredit = _securedLineOfCreditData.GetById(id);
                if (securedLineOfCredit != null)
                {
                    var removeStatus = _securedLineOfCreditData.RemoveSecuredLineOfCredit(securedLineOfCredit);
                    if (removeStatus == 0)
                    {
                        // remove succeeded
                        var commitStatus = _securedLineOfCreditData.Commit();
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
                        // remove fialed
                        return RedirectToAction("Index");

                    }

                }
                // loc is null
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: HomeEquityLinesOfCredit/Details/5
        public IActionResult Details(int id)
        {
            // Refactor
            var securedLineOfCredit = _securedLineOfCreditData.GetById(id);
            if(securedLineOfCredit != null)
            {
                return View(securedLineOfCredit);

            }
            // loc is null
            return RedirectToAction("Index");
        }

        // GET: HomeEquityLinesOfCredit/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            // TODO: Refactor
            var securedLineOfCredit = _securedLineOfCreditData.GetById(id);
            if (securedLineOfCredit != null)
            {
                TempData["SecuredLineOfCreditId"] = id;
                return View(securedLineOfCredit);

            }
            // Replace With 404 Page
            return RedirectToAction("Index");
        }

        // POST: HomeEquityLinesOfCredit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(double? collateralToLocRatio, double? lowApr,
            double? highApr, double? minimumAmount, double? maximumAmount,
            int? minimumTermInMonths, int? maximumTermInMonths, bool hasOriginationFee,
            double? originationFee, bool hasAnnualFee, double? annualFee, bool hasAdvanceFee,
            double? advanceFee, string name, bool arePaymentsInterestOnly, double? minimumPayment,
            string linkURL)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["SecuredLineOfCreditId"];
                var loc = _securedLineOfCreditData.GetById(id);
                if (loc != null)
                {
                    loc.CollateralToLocRatio = collateralToLocRatio;
                    loc.LowApr = lowApr;
                    loc.HighApr = highApr;
                    loc.MinimumAmount = minimumAmount;
                    loc.MaximumAmount = maximumAmount;
                    loc.MinimumTermInMonths = minimumTermInMonths;
                    loc.MaximumTermInMonths = maximumTermInMonths;
                    loc.HasOriginationFee = hasOriginationFee;
                    loc.OriginationFee = originationFee;
                    loc.HasAnnualFee = hasAnnualFee;
                    loc.AnnualFee = annualFee;
                    loc.HasAdvanceFee = hasAdvanceFee;
                    loc.AdvanceFee = advanceFee;
                    loc.Name = name;
                    loc.ArePaymentsInterestOnly = arePaymentsInterestOnly;
                    loc.MinimumPayment = minimumPayment;
                    loc.LinkURL = linkURL;

                    var commitStatus = _securedLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = loc.Id });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");
                    }
                    


                }
                // loc is null
                return RedirectToAction("Index");

            }

            return View();
        }
    }
}