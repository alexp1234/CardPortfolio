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
using Microsoft.Extensions.Logging;

namespace CardPortfolio.Controllers
{
    public class AutoLoansController : Controller
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IAutoLoanData _autoLoanData;
        
        public AutoLoansController( IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment, IAutoLoanData autoLoanData )
        {
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;
            _autoLoanData = autoLoanData;

        }
        public IActionResult Index()
        {
            // TODO: Refactor
            var autoLoanList = _autoLoanData.GetAllAutoLoans();
            return View(autoLoanList);
            

        }

        // GET: AutoLoans/AddImage/3
        [Authorize(Roles="Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["AutoLoanId"] = id;
            return View();

        }

        // POST: AutoLoans/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult AddImage(AddAutoLoanImageViewModel model)
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
                int loanId = (int)TempData["AutoLoanId"];
               
                var loan = _autoLoanData.GetAutoLoanById(loanId);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;                    
                    var status = _autoLoanData.Commit();
                    if(status == 0)
                    {
                        return RedirectToAction("Details", new { id = (int)TempData["AutoLoanId"] });
                    }
                    else
                    {
                        // Replace with error completing task page
                        return RedirectToAction("Index");

                    }
                }
                
            }
            return RedirectToAction("Index");
        }

        // GET: AutoLoans/Create
        [Authorize(Roles ="Administrator")]
        public IActionResult Create()
        {
            ViewBag.AutoLoanCategoryList = _htmlHelper.GetEnumSelectList<AutoLoanCategory>();
            return View();
        }

        // POST: AutoLoans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult Create(double? lowApr, double? highApr, double? minimumAmount, double? maximumAmount,
            int? minimumTermInMonths, int? maximumTermInMonths, bool hasFees, double? originationFee, 
            string name, AutoLoanCategory autoLoanCategory, double? downPaymentPercentage, AutoLoan autoLoan )
        {
            if (ModelState.IsValid)
            {
                autoLoan.LowApr = lowApr;
                autoLoan.HighApr = highApr;
                autoLoan.MinimumAmount = minimumAmount;
                autoLoan.MinimumAmount = maximumAmount;
                autoLoan.MinimumTermInMonths = minimumTermInMonths;
                autoLoan.MaximumTermInMonths = maximumTermInMonths;
                autoLoan.HasFees = hasFees;
                autoLoan.OriginationFee = originationFee;
                autoLoan.Name = name;
                autoLoan.AutoLoanCategory = autoLoanCategory;
                autoLoan.DownPaymentPercentage = downPaymentPercentage;

                
               var addStatus = _autoLoanData.AddAutoLoan(autoLoan);
               if(addStatus == 0)
                {
                    var status = _autoLoanData.Commit();
                    if(status == 0)
                    {
                        return RedirectToAction("Details", new { id = autoLoan.Id });
                    }
                    else
                    {
                        // replace with error saving changes 
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Replace with error adding autoLoan page
                    return RedirectToAction("Index");
                }
                
            }
            return RedirectToAction("Index");
        }

        // GET: AutoLoans/Delete/5
        [Authorize(Roles ="Administrator")]
        public IActionResult Delete(int id)
        {
            
            var autoLoan = _autoLoanData.GetAutoLoanById(id);
            if(autoLoan != null)
            {
                TempData["AutoLoanId"] = id;
                return View(autoLoan);
            }
            return RedirectToAction("Index");
        }

        // POST: AutoLoans/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult Delete()
        {
            int id = (int)TempData["AutoLoanId"];
            var autoLoan = _autoLoanData.GetAutoLoanById(id);
            if (autoLoan != null)
            {
                // TODO: Refactor
               var removeStatus = _autoLoanData.RemoveAutoLoan(autoLoan);
                if(removeStatus == 0)
                {
                    var status = _autoLoanData.Commit();
                    if(status == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Replace with error Saving changes page
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Replace with error deleting item page
                    return RedirectToAction("Index");
                }
                
                
            }
            return View();
        }


        // GET: AutoLoans/Details/5
        public IActionResult Details(int id)
        {
            // Refactor
            var autoLoan = _autoLoanData.GetAutoLoanById(id);
            if(autoLoan != null)
            {
                return View(autoLoan);
            }
            return RedirectToAction("Index");
            
        }

        // GET: AutoLoans/Edit/5
        [Authorize(Roles ="Administrator")]
        public IActionResult Edit(int id)
        {
            // TODO: Refactor
            var autoLoan = _autoLoanData.GetAutoLoanById(id);
            if(autoLoan != null)
            {

                TempData["AutoLoanId"] = id;
                ViewBag.AutoLoanCategoryList = _htmlHelper.GetEnumSelectList<AutoLoanCategory>();
                return View(autoLoan);

            }
            // Replace With 404 Page
            return RedirectToAction("Index");
        }

        // POST: AutoLoans/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public IActionResult Edit(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minTermInMonths, int? maxTermInMonths, bool hasFees,
            double? originationFee, string name, AutoLoanCategory autoLoanCategory, double? downPaymentPercentage  )
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["AutoLoanId"];
                var autoLoan = _autoLoanData.GetAutoLoanById(id);
                if(autoLoan != null)
                {
                    autoLoan.LowApr = lowApr;
                    autoLoan.HighApr = highApr;
                    autoLoan.MinimumAmount = minimumAmount;
                    autoLoan.MaximumAmount = maximumAmount;
                    autoLoan.MinimumTermInMonths = minTermInMonths;
                    autoLoan.MaximumTermInMonths = maxTermInMonths;
                    autoLoan.HasFees = hasFees;
                    autoLoan.OriginationFee = originationFee;
                    autoLoan.Name = name;
                    autoLoan.AutoLoanCategory = autoLoanCategory;
                    autoLoan.DownPaymentPercentage = downPaymentPercentage;
                    var status = _autoLoanData.Commit();
                    if(status == 0)
                    {
                        return RedirectToAction("Details", new { id = autoLoan.Id });
                    }
                    else
                    {

                    }
                }
               
            }
            return RedirectToAction("Index");
        }

    }
}