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
    public class UnsecuredPersonalLoansController : Controller
    {
        private readonly IUnsecuredPersonalLoanData _unsecuredPersonalLoanData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public UnsecuredPersonalLoansController(IUnsecuredPersonalLoanData unsecuredPersonalLoanData, IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment)
        {
            _unsecuredPersonalLoanData = unsecuredPersonalLoanData;
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;

        }
        public IActionResult Index()
        {
            var loans = _unsecuredPersonalLoanData.GetAllUnsecuredPersonalLoans();
            return View(loans);
        }

        // GET: HomeEquityLinesOfCredit/AddImage/3
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["UnsecuredLoanId"] = id;
            return View();

        }

        // POST: HomeEquityLinesOfCredit/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddUnsecuredLoanImageViewModel model)
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
                int loanId = (int)TempData["UnsecuredLoanId"];
                var loan = _unsecuredPersonalLoanData.GetById(loanId);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;
                    var commitStatus = _unsecuredPersonalLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = (int)TempData["UnsecuredLoanId"] });

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
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minimumTermInMonths, int? maximumTermInMonths,
            bool hasFees, double? originationFee, string name, UnsecuredPersonalLoan loan)
        {
            if (ModelState.IsValid)
            {
                loan.LowApr = lowApr;
                loan.HighApr = highApr;
                loan.MinimumAmount = minimumAmount;
                loan.MaximumAmount = maximumAmount;
                loan.MinimumTermInMonths = minimumTermInMonths;
                loan.MaximumTermInMonths = maximumTermInMonths;
                loan.HasFees = hasFees;
                loan.OriginationFee = originationFee;
                loan.Name = name;

                var addStatus = _unsecuredPersonalLoanData.AddUnsecuredPersonalLoan(loan);
                if(addStatus == 0)
                {
                    // add succeeded
                    var commitStatus = _unsecuredPersonalLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = loan.Id });

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
            TempData["UnsecuredLoanId"] = id;
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
                int id = (int)TempData["UnsecuredLoanId"];
                var loan = _unsecuredPersonalLoanData.GetById(id);
                if (loan != null)
                {
                    var removeStatus = _unsecuredPersonalLoanData.DeleteUnsecuredPersonalLoan(loan);
                    if (removeStatus == 0)
                    {
                        var commitStatus = _unsecuredPersonalLoanData.Commit();
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
                        // remove Failed
                        return RedirectToAction("Index");
                    }

                }
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: HomeEquityLinesOfCredit/Details/5
        public IActionResult Details(int id)
        {
            // Refactor
            var loan = _unsecuredPersonalLoanData.GetById(id);
            return View(loan);
        }

        // GET: HomeEquityLinesOfCredit/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            // TODO: Refactor
            var loan = _unsecuredPersonalLoanData.GetById(id);
            if (loan != null)
            {
                TempData["UnsecuredLoanId"] = id;
                return View(loan);

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
            bool hasFees, double? originationFee, string name)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["UnsecuredLoanId"];
                var loan = _unsecuredPersonalLoanData.GetById(id);
                if (loan != null)
                {
                    loan.LowApr = lowApr;
                    loan.HighApr = highApr;
                    loan.MinimumAmount = minimumAmount;
                    loan.MaximumAmount = maximumAmount;
                    loan.MinimumTermInMonths = minimumTermInMonths;
                    loan.MaximumTermInMonths = maximumTermInMonths;
                    loan.HasFees = hasFees;
                    loan.OriginationFee = originationFee;
                    loan.Name = name;

                    var commitStatus = _unsecuredPersonalLoanData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = loan.Id });

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
            return View();

            
        }
    }
}
