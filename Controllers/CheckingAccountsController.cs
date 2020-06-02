using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardPortfolio.Data;
using CardPortfolio.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardPortfolio.Controllers
{
    public class CheckingAccountsController : Controller
    {
        private readonly ICheckingAccountData _checkingAccountData;
        public CheckingAccountsController(ICheckingAccountData checkingAccountData)
        {
            _checkingAccountData = checkingAccountData;
        }
        // GET: CheckingAccounts
        public ActionResult Index()
        {
            var list = _checkingAccountData.GetAllCheckingAccounts();
            return View(list);
        }

       

        // GET: CheckingAccounts/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: CheckingAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(double interestRate,
            bool hasMinimumToOpen, double? minimumToOpenAccount, bool hasMonthlyFee,
            double monthlyFee, double? balanceToAvoidFee, bool hasMinimumForApr, 
            double? minimumAmountForApr, bool hasMaximumAmountForApr, double? maximumAmountForApr,
            double? aprIfAmountNotMet, bool hasDirectDepositRequirementForAPR, double? directDepositRequirementForAPR, 
            double? interestRateIfDDRequirementNotMet, double? directDepositToAvoidMonthlyFee, string name,
            string linkURL, CheckingAccount checkingAccount)
        {
            if (ModelState.IsValid)
            {
                checkingAccount.InterestRate = interestRate;
                checkingAccount.HasMinimumToOpenAccount = hasMinimumToOpen;
                checkingAccount.MinimumToOpenAccount = minimumToOpenAccount;
                checkingAccount.HasMonthlyFee = hasMonthlyFee;
                checkingAccount.MonthlyFee = monthlyFee;
                checkingAccount.BalanceToAvoidFee = balanceToAvoidFee;
                checkingAccount.HasMinimumAmountForApr = hasMinimumForApr;
                checkingAccount.MinimumAmountForApr = minimumAmountForApr;
                checkingAccount.HasMaximumAmountForApr = hasMaximumAmountForApr;
                checkingAccount.MaximumAmountForApr = maximumAmountForApr;
                checkingAccount.AprIfAmountNotMet = aprIfAmountNotMet;
                checkingAccount.HasDirectDepositRequirementForAPR = hasDirectDepositRequirementForAPR;
                checkingAccount.DirectDepositRequirementsForAPR = directDepositRequirementForAPR;
                checkingAccount.InterestRateIfDDRequirementsNotMet = interestRateIfDDRequirementNotMet;
                checkingAccount.DirectDepositToAvoidMonthlyFee = directDepositToAvoidMonthlyFee;
                checkingAccount.Name = name;
                checkingAccount.LinkURL = linkURL;
                var addStatus = _checkingAccountData.AddCheckingAccount(checkingAccount);
                if(addStatus == 0)
                {
                    var commitStatus = _checkingAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                return RedirectToAction("Index");
             
                
            }
            return View();
        }

        // GET: CheckingAccounts/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var account = _checkingAccountData.GetById(id);
            if(account != null)
            {
                TempData["AccountId"] = id;
                return View(account);
            }
            return RedirectToAction("Index");
        }

        // POST: CheckingAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(double interestRate,
            bool hasMinimumToOpen, double? minimumToOpenAccount, bool hasMonthlyFee,
            double monthlyFee, double? balanceToAvoidFee, bool hasMinimumForApr,
            double? minimumAmountForApr, bool hasMaximumAmountForApr, double? maximumAmountForApr,
            double? aprIfAmountNotMet, bool hasDirectDepositRequirementForAPR, double? directDepositRequirementForAPR,
            double? interestRateIfDDRequirementNotMet, double? directDepositToAvoidMonthlyFee, string name, string linkURL)
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["AccountId"];
                var checkingAccount = _checkingAccountData.GetById(id);
                if(checkingAccount != null)
                {
                    checkingAccount.InterestRate = interestRate;
                    checkingAccount.HasMinimumToOpenAccount = hasMinimumToOpen;
                    checkingAccount.MinimumToOpenAccount = minimumToOpenAccount;
                    checkingAccount.HasMonthlyFee = hasMonthlyFee;
                    checkingAccount.MonthlyFee = monthlyFee;
                    checkingAccount.BalanceToAvoidFee = balanceToAvoidFee;
                    checkingAccount.HasMinimumAmountForApr = hasMinimumForApr;
                    checkingAccount.MinimumAmountForApr = minimumAmountForApr;
                    checkingAccount.HasMaximumAmountForApr = hasMaximumAmountForApr;
                    checkingAccount.MaximumAmountForApr = maximumAmountForApr;
                    checkingAccount.AprIfAmountNotMet = aprIfAmountNotMet;
                    checkingAccount.HasDirectDepositRequirementForAPR = hasDirectDepositRequirementForAPR;
                    checkingAccount.DirectDepositRequirementsForAPR = directDepositRequirementForAPR;
                    checkingAccount.InterestRateIfDDRequirementsNotMet = interestRateIfDDRequirementNotMet;
                    checkingAccount.DirectDepositToAvoidMonthlyFee = directDepositToAvoidMonthlyFee;
                    checkingAccount.Name = name;
                    checkingAccount.LinkURL = linkURL;
                    var commitStatus = _checkingAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                    
                }
                
            }
            return View();
        }

        // GET: CheckingAccounts/Delete/5
        [Authorize(Roles="Administrator")]
        public ActionResult Delete(int id)
        {
            var account = _checkingAccountData.GetById(id);
            if(account != null)
            {
                TempData["AccountId"] = id;
                return View(account);
            }
            return RedirectToAction("Index");

            
        }

        // POST: CheckingAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["AccountId"];
                var account = _checkingAccountData.GetById(id);
                if(account != null)
                {
                    var removeStatus = _checkingAccountData.RemoveCheckingAccount(account);
                    if(removeStatus == 0)
                    {
                        var commitStatus = _checkingAccountData.Commit();
                        if(commitStatus == 0)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                    
                    
                }
                return RedirectToAction("Index");
                
            }
            return View();
        }

        // GET: AutoLoans/RemoveLoan/5
        [Authorize(Roles = "Administrator")]
        public IActionResult Remove(int id)
        {
            var item = _checkingAccountData.GetById(id);
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
                var item = _checkingAccountData.GetById(id);
                if (item != null)
                {
                    var institutionId = item.InstitutionId;
                    item.InstitutionId = null;
                    var commitStatus = _checkingAccountData.Commit();
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