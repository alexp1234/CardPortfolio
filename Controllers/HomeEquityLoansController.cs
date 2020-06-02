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
    public class HomeEquityLoansController : Controller
    {
        private readonly IHomeEquityLoanData _homeEquityLoanData;
        private IHtmlHelper _htmlHelper;
        private IHostingEnvironment _hostingEnvironment;
        public HomeEquityLoansController(IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment, IHomeEquityLoanData homeEquityLoanData)
        {
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
            _homeEquityLoanData = homeEquityLoanData;

        }

        // GET: HomeEquityLoans/Index
        public IActionResult Index()
        {
            // TODO: Refactor
            var heloanList = _homeEquityLoanData.GetAllHomeEquityLoans();
            return View(heloanList);
        }

        // GET: HomeEquityLoans/AddImage/3
        [Authorize(Roles= "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["HomeEquityLoanId"] = id;
            return View();

        }

        // POST: HomeEquityLoans/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddHomeEquityLoanImageViewModel model)
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
                int heloanId = (int)TempData["HomeEquityLoanId"];
                // TODO: Refactor
                var loan = _homeEquityLoanData.GetById(heloanId);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;
                    var commitStatus = _homeEquityLoanData.Commit();
                    if (commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = (int)TempData["HomeEquityLoanId"] });
                    }
                    else
                    {
                        // error to save message
                    }
                }
               
            }
            return RedirectToAction("Index");
        }

        // GET: HomeEquityLoans/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeEquityLoans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minimumTermInMonths, int? maximumTermInMonths, bool hasFees,
            double? originationFee, string name, double Ltv, string linkURL, HomeEquityLoan heLoan)
        {
            if (ModelState.IsValid)
            {
                heLoan.LowApr = lowApr;
                heLoan.HighApr = highApr;
                heLoan.MinimumAmount = minimumAmount;
                heLoan.MaximumAmount = maximumAmount;
                heLoan.MinimumTermInMonths = minimumTermInMonths;
                heLoan.MaximumTermInMonths = maximumTermInMonths;
                heLoan.HasFees = hasFees;
                heLoan.OriginationFee = originationFee;
                heLoan.Name = name;
                heLoan.LTV = Ltv;
                heLoan.LinkURL = linkURL;
                var addStatus = _homeEquityLoanData.AddHomeEquityLoan(heLoan);
                if(addStatus == 0)
                {
                    var commitStatus = _homeEquityLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = heLoan.Id });
                    }
                    else
                    {
                        // error message
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // error message
                    return RedirectToAction("Index");
                }
                

            }
            // Replace With something went wrong page 
            return RedirectToAction("Index");
        }

        // GET: HomeEquityLoans/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            TempData["HomeEquityLoanId"] = id;
            return View();
        }

        // POST: AutoLoans/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete()
        {
            int id = (int)TempData["HomeEquityLoanId"];
            var heLoan = _homeEquityLoanData.GetById(id);
            if (heLoan != null)
            {
                // TODO: Refactor
                var removeStatus = _homeEquityLoanData.RemoveHomeEquityLoan(heLoan);
                if(removeStatus == 0)
                {
                    var commitStatus = _homeEquityLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // error to save message
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // error removing message
                    return RedirectToAction("Index");
                }

            }
            return View();
        }


        // GET: HomeEquityLoans/Details/5
        public IActionResult Details(int id)
        {
            // Refactor
            var heLoan = _homeEquityLoanData.GetById(id);
            if(heLoan != null)
            {
                return View(heLoan);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: HomeEquityLoans/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            // TODO: Refactor
            var heLoan = _homeEquityLoanData.GetById(id);
            if (heLoan != null)
            {
                TempData["HomeEquityLoanId"] = id;
                return View(heLoan);

            }
            // Replace With 404 Page
            return RedirectToAction("Index");
        }

        // POST: AutoLoans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minimumTermInMonths, int? maximumTermInMonths, 
            bool hasFees, double? originationFee, string name, double Ltv, string linkURL)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["HomeEquityLoanId"];
                var heLoan = _homeEquityLoanData.GetById(id);
                if (heLoan != null)
                {
                    heLoan.LowApr = lowApr;
                    heLoan.HighApr = highApr;
                    heLoan.MinimumAmount = minimumAmount;
                    heLoan.MaximumAmount = maximumAmount;
                    heLoan.MinimumTermInMonths = minimumTermInMonths;
                    heLoan.MaximumTermInMonths = maximumTermInMonths;
                    heLoan.HasFees = hasFees;
                    heLoan.OriginationFee = originationFee;
                    heLoan.Name = name;
                    heLoan.LTV = Ltv;
                    heLoan.LinkURL = linkURL;
                    var commitStatus = _homeEquityLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = heLoan.Id });
                    }
                    else
                    {
                        // Error saving changes
                        return RedirectToAction("Index");
                    }
                    

                }

            }
            return RedirectToAction("Index");
        }

    }
}