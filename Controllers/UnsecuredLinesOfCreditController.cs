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
    public class UnsecuredLinesOfCreditController : Controller
    {
        private readonly IUnsecuredLineOfCreditData _unsecuredLineOfCreditData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UnsecuredLinesOfCreditController(IUnsecuredLineOfCreditData unsecuredLineOfCreditData, IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment)
        {
            _unsecuredLineOfCreditData = unsecuredLineOfCreditData;
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {
            // TODO: Refactor
            var loc = _unsecuredLineOfCreditData.GetAllUnsecuredLinesOfCredit();
            return View(loc);
        }

        // GET: HomeEquityLinesOfCredit/AddImage/3
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["UnsecuredLineOfCreditId"] = id;
            return View();

        }

        // POST: HomeEquityLinesOfCredit/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddUnsecuredLineOfCreditImageViewModel model)
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
                int loc = (int)TempData["UnsecuredLineOfCreditId"];
                var loan = _unsecuredLineOfCreditData.GetById(loc);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;
                    var commitStatus = _unsecuredLineOfCreditData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = (int)TempData["UnsecuredLineOfCreditId"] });

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
            return View();
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
        public IActionResult Create(double? lowApr, double? highApr,
            double? minimumAmount, double? maximumAmount, int? minimumTerm,
            int? maximumTerm, bool hasOriginationFee, double? originationFee, bool hasAnnualFee,
            double? annualFee, bool hasAdvanceFee, double? advanceFee, string name,
            bool arePaymentsInterestOnly, double? minimumPayment, string linkURL, UnsecuredLineOfCredit loc)
        {
            if (ModelState.IsValid)
            {
                loc.LowApr = lowApr;
                loc.HighApr = highApr;
                loc.MinimumAmount = minimumAmount;
                loc.MaximumAmount = maximumAmount;
                loc.MinimumTermInMonths = minimumTerm;
                loc.MaximumTermInMonths = maximumTerm;
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
                var addStatus = _unsecuredLineOfCreditData.AddUnsecuredLineOfCredit(loc);
                if(addStatus == 0)
                {
                    // add succeeded
                    var commitStatus = _unsecuredLineOfCreditData.Commit();
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
                else
                {
                    // add failed
                    return RedirectToAction("Index");

                }

                
            }
            return View();
        }

        // GET: HomeEquityLinesOfCredit/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            TempData["UnsecuredLineOfCreditId"] = id;
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
                int id = (int)TempData["UnsecuredLineOfCreditId"];
                var unsecuredLineOfCredit = _unsecuredLineOfCreditData.GetById(id);
                if (unsecuredLineOfCredit != null)
                {
                    var removeStatus = _unsecuredLineOfCreditData.RemoveUnsecuredLineOfCredit(unsecuredLineOfCredit);
                    if(removeStatus == 0)
                    {
                        // remove succeeded
                        var commitStatus = _unsecuredLineOfCreditData.Commit();
                        if(commitStatus == 0)
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
                        // remove failed
                        return RedirectToAction("Index");
                    }
                    
                }
                else
                {
                    // loc is null
                    return RedirectToAction("Index");
                }
                
            }
            return View();
        }


        // GET: HomeEquityLinesOfCredit/Details/5
        public IActionResult Details(int id)
        {
            // Refactor
            var unsecuredLineOfCredit = _unsecuredLineOfCreditData.GetById(id);
            return View(unsecuredLineOfCredit);
        }

        // GET: HomeEquityLinesOfCredit/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            // TODO: Refactor
            var unsecuredLineOfCredit = _unsecuredLineOfCreditData.GetById(id);
            if (unsecuredLineOfCredit != null)
            {
                TempData["UnsecuredLineOfCreditId"] = id;
                return View(unsecuredLineOfCredit);

            }
            // Replace With 404 Page
            return RedirectToAction("Index");
        }

        // POST: HomeEquityLinesOfCredit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(double? lowApr, double? highApr,
            double? minimumAmount, double? maximumAmount, int? minimumTerm,
            int? maximumTerm, bool hasOriginationFee, double? originationFee, bool hasAnnualFee,
            double? annualFee, bool hasAdvanceFee, double? advanceFee, string name,
            bool arePaymentsInterestOnly, double? minimumPayment, string linkURL)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["UnsecuredLineOfCreditId"];
                var loc = _unsecuredLineOfCreditData.GetById(id);
                if (loc != null)
                {
                    
                    loc.LowApr = lowApr;
                    loc.HighApr = highApr;
                    loc.MinimumAmount = minimumAmount;
                    loc.MaximumAmount = maximumAmount;
                    loc.MinimumTermInMonths = minimumTerm;
                    loc.MaximumTermInMonths = maximumTerm;
                    loc.HasOriginationFee = hasOriginationFee;
                    loc.OriginationFee = originationFee;
                    loc.HasAnnualFee = hasAnnualFee;
                    loc.AnnualFee = annualFee;
                    loc.HasAdvanceFee = hasAdvanceFee;
                    loc.AdvanceFee = advanceFee;
                    loc.Name = name;
                    loc.ArePaymentsInterestOnly = arePaymentsInterestOnly;
                    loc.MinimumPayment = minimumPayment;

                    var commitStatus = _unsecuredLineOfCreditData.Commit();
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
            // Model State not valid
            return View();
            
        }
    }
}