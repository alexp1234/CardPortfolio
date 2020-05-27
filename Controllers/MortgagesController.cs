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
    public class MortgagesController : Controller
    {
        private readonly IMortgageData _mortgageData;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public MortgagesController(ApplicationDbContext db, IHtmlHelper htmlHelper, IHostingEnvironment hostingEnvironment, IMortgageData mortgageData)
        {
            _mortgageData = mortgageData;
            _htmlHelper = htmlHelper;
            _hostingEnvironment = hostingEnvironment;

        }

        // GET: Mortgages/Index
        public IActionResult Index()
        {
            // TODO: Refactor
            var mortgageList = _mortgageData.GetAllMortgages();
            return View(mortgageList);
        }

        // GET: Mortgages/AddImage/3
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(int id)
        {

            TempData["MortgageId"] = id;
            return View();

        }

        // POST: Mortgages/AddImage/3
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddImage(AddMortgageImageViewModel model)
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
                int mortgageId = (int)TempData["MortgageId"];
                // TODO: Refactor
                var loan = _mortgageData.GetById(mortgageId);
                if (loan != null)
                {
                    loan.ImageUrl = uniqueFileName;
                    var commitStatus = _mortgageData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = (int)TempData["MortgageId"] });

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

        // GET: Mortgages/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            ViewBag.MortgageTypeList = _htmlHelper.GetEnumSelectList<MortgageType>();
            return View();
        }

        // POST: Mortgages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create(double? lowApr, double? highApr, int? institutionId, 
            double? minimumAmount, double? maximumAmount, int? minimumTermInMonths,
            int? maximumTermInMonths, bool hasFees, double? originationFee, string name, double? loanToValue,
            MortgageType mortgageType, double? downPaymentPercent, Mortgage mortgage)
        {
            if (ModelState.IsValid)
            {
                mortgage.LowApr = lowApr;
                mortgage.HighApr = highApr;
                mortgage.InstitutionId = institutionId;
                mortgage.MinimumAmount = minimumAmount;
                mortgage.MaximumAmount = maximumAmount;
                mortgage.MinimumTermInMonths = minimumTermInMonths;
                mortgage.MaximumTermInMonths = maximumTermInMonths;
                mortgage.HasFees = hasFees;
                mortgage.OriginationFee = originationFee;
                mortgage.Name = name;
                mortgage.LoanToValue = loanToValue;
                mortgage.MortgageType = mortgageType;
                mortgage.DownPaymentPercentage = downPaymentPercent;
                var addStatus = _mortgageData.AddMortgage(mortgage);
                if(addStatus == 0)
                {
                    // add succeeded
                    var commitStatus = _mortgageData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = mortgage.Id });

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
            // Replace With something went wrong page 
            return RedirectToAction("Index");
        }

        // GET: Mortgages/Delete/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            TempData["MortgageId"] = id;
            return View();
        }

        // POST: Mortgages/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete()
        {
            if (ModelState.IsValid)
            {


                int id = (int)TempData["MortgageId"];
                var mortgage = _mortgageData.GetById(id);
                if (mortgage != null)
                {
                    var removeStatus = _mortgageData.RemoveMortage(mortgage);
                    if (removeStatus == 0)
                    {
                        // remove succeeded
                        var commitStatus = _mortgageData.Commit();
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
                        // remove failed 
                        return RedirectToAction("Index");
                    }


                }
            }
            return View();
        }


        // GET: Mortgages/Details/5
        public IActionResult Details(int id)
        {
            var mortgage = _mortgageData.GetById(id);
            return View(mortgage);
        }

        // GET: Mortgages/Edit/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int id)
        {
            var mortgage = _mortgageData.GetById(id);
            if (mortgage != null)
            {
                TempData["MortgageId"] = id;
                ViewBag.MortgageTypeList = _htmlHelper.GetEnumSelectList<MortgageType>();
                return View(mortgage);

            }
            // Replace With 404 Page
            return RedirectToAction("Index");
        }

        // POST: Mortgages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(double? lowApr, double? highApr, double? minimumAmount,
            double? maximumAmount, int? minimumTermInMonths, int? maximumTermInMonths,
            bool hasFees, double? originationFee, string name, double loanToValue, 
            MortgageType mortgageType, double? downPaymentPercentage)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["MortgageId"];
                var mortgage = _mortgageData.GetById(id);
                if (mortgage != null)
                {
                    mortgage.LowApr = lowApr;
                    mortgage.HighApr = highApr;
                    mortgage.MinimumAmount = minimumAmount;
                    mortgage.MaximumAmount = maximumAmount;
                    mortgage.MinimumTermInMonths = minimumTermInMonths;
                    mortgage.MaximumTermInMonths = maximumTermInMonths;
                    mortgage.HasFees = hasFees;
                    mortgage.OriginationFee = originationFee;
                    mortgage.Name = name;
                    mortgage.LoanToValue = loanToValue;
                    mortgage.MortgageType = mortgageType;
                    mortgage.DownPaymentPercentage = downPaymentPercentage;
                    var commitStatus = _mortgageData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded 
                        return RedirectToAction("Details", new { id = mortgage.Id });

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
    }
}