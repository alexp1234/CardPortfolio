using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CardPortfolio.Data;
using CardPortfolio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardPortfolio.Controllers
{
    public class CertificateAccountsController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICertificateAccountData _certificateAccountData;

        public CertificateAccountsController(IHostingEnvironment hostingEnvironment, ICertificateAccountData certificateAccountData)
        {

            _hostingEnvironment = hostingEnvironment;
            _certificateAccountData = certificateAccountData;
        }
        // GET: CertificateAccounts
        public ActionResult Index()
        {
            var list = _certificateAccountData.GetAllCertificateAccounts();
            return View(list);
        }

        // GET: CertificateAccounts/Details/5
        public ActionResult Details(int id)
        {
            var certificateAccount = _certificateAccountData.GetById(id);
            if(certificateAccount != null)
            {
                return View(certificateAccount);
            }

            // TODO: Replace with 404 page
            return RedirectToAction("Index");
        }

       

        // GET: CertificateAccounts/Create
        [Authorize(Roles ="Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: CertificateAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(double interestRate, bool hasMinimumToOpenAccount,
            double? minimumToOpenAccount, bool hasMonhtlyFee, double monthlyFee,
            double? balanceToAvoidFee, bool hasMinimumAmountForApr,
            double? minimumAmountForApr, bool hasMaximimumAmountForApr, double? maximumAmountForApr,
            double? aprIfAmountNotMet, double termInMonths, string name, CertificateAccount certificateAccount)
        {
            if (ModelState.IsValid)
            {
                certificateAccount.InterestRate = interestRate;
                certificateAccount.HasMinimumToOpenAccount = hasMinimumToOpenAccount;
                certificateAccount.MinimumToOpenAccount = minimumToOpenAccount;
                certificateAccount.HasMonthlyFee = hasMonhtlyFee;
                certificateAccount.MonthlyFee = monthlyFee;
                certificateAccount.BalanceToAvoidFee = balanceToAvoidFee;
                certificateAccount.HasMinimumAmountForApr = hasMinimumAmountForApr;
                certificateAccount.MinimumAmountForApr = minimumAmountForApr;
                certificateAccount.HasMaximumAmountForApr = hasMinimumAmountForApr;
                certificateAccount.MaximumAmountForApr = maximumAmountForApr;
                certificateAccount.AprIfAmountNotMet = aprIfAmountNotMet;
                certificateAccount.TermInMonths = termInMonths;
                certificateAccount.Name = name;
                var addStatus = _certificateAccountData.AddCertificateAccount(certificateAccount);
                if(addStatus == 0)
                {
                    var commitStatus = _certificateAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", "CertificateAccounts", new { id = certificateAccount.Id });
                    }
                    else
                    {
                        // add logging message
                    }
                }
                else
                {
                    // add logging message
                }
                
               
            }
            return View();
        }

        // GET: CertificateAccounts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var account = _certificateAccountData.GetById(id);
            if (account != null)
            {
                TempData["AccountId"] = id;
                return View(account);
            }
            // TODO: Replace with "Something went wrong page"
            return RedirectToAction("Index");
            
        }

        // POST: CertificateAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(double interestRate, bool hasMinimumToOpenAccount,
            double? minimumToOpenAccount, bool hasMonhtlyFee, double monthlyFee,
            double? balanceToAvoidFee, bool hasMinimumAmountForApr,
            double? minimumAmountForApr, bool hasMaximimumAmountForApr, double? maximumAmountForApr,
            double? aprIfAmountNotMet, double termInMonths, string name)
        {
            if (ModelState.IsValid)
            {
                int id = (int)TempData["AccountId"];
                var certificateAccount = _certificateAccountData.GetById(id);
                if (certificateAccount != null)
                {
                    certificateAccount.InterestRate = interestRate;
                    certificateAccount.HasMinimumToOpenAccount = hasMinimumToOpenAccount;
                    certificateAccount.MinimumToOpenAccount = minimumToOpenAccount;
                    certificateAccount.HasMonthlyFee = hasMonhtlyFee;
                    certificateAccount.MonthlyFee = monthlyFee;
                    certificateAccount.BalanceToAvoidFee = balanceToAvoidFee;
                    certificateAccount.HasMinimumAmountForApr = hasMinimumAmountForApr;
                    certificateAccount.MinimumAmountForApr = minimumAmountForApr;
                    certificateAccount.HasMaximumAmountForApr = hasMinimumAmountForApr;
                    certificateAccount.MaximumAmountForApr = maximumAmountForApr;
                    certificateAccount.AprIfAmountNotMet = aprIfAmountNotMet;
                    certificateAccount.TermInMonths = termInMonths;
                    certificateAccount.Name = name;
                    var commitStatus = _certificateAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Details", new { id = certificateAccount.Id });
                    }
                    else
                    {
                        // add logging message
                        return RedirectToAction("Index");
                    }
                    
                }
            }
            return View();
            
        }

        // GET: CertificateAccounts/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var account = _certificateAccountData.GetById(id);
            if(account != null)
            {
                TempData["AccountId"] = id;

            }
            return View(account);
        }

        // POST: CertificateAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["AccountId"];
                var account = _certificateAccountData.GetById(id);
                if (account != null)
                {
                    var removeStatus = _certificateAccountData.RemoveCertificateAccount(account);
                    if(removeStatus == 0)
                    {
                        // remove succeeded
                        var commitStatus = _certificateAccountData.Commit();
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
            }
            return View();
        }


        // GET: AutoLoans/RemoveLoan/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Remove(int id)
        {
            var item = _certificateAccountData.GetById(id);
            if (item != null)
            {
                TempData["Id"] = id;
                return View(item);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: AutoLoans/RemoveLoan/5
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["Id"];
                var item = _certificateAccountData.GetById(id);
                if (item != null)
                {
                    var institutionId = item.InstitutionId;
                    item.InstitutionId = null;
                    var commitStatus = _certificateAccountData.Commit();
                    if (commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", "Institutions", new { id = institutionId });

                    }
                    else
                    {
                        // commit failed;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // loan is null
                    return RedirectToAction("Index");
                }
            }
            // Model state not valid
            return View();
        }
    }

}