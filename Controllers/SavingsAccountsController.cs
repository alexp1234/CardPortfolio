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
    public class SavingsAccountsController : Controller
    {
        private readonly ISavingsAccountData _savingsAccountData;
        public SavingsAccountsController(ISavingsAccountData savingsAccountData)
        {
            _savingsAccountData = savingsAccountData;
        }
        // GET: SavingsAccounts
        public ActionResult Index()
        {
            var list = _savingsAccountData.GetAllSavingsAccounts();
            return View(list);
        }

        // GET: SavingsAccounts/Details/5
        public ActionResult Details(int id)
        {
            var account = _savingsAccountData.GetById(id);
            if(account != null)
            {
                return View(account);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: SavingsAccounts/Create
        [Authorize(Roles="Administrator")]
        public ActionResult Create()
        {

            return View();
        }

        // POST: SavingsAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Administrator")]
        public ActionResult Create(double interestRate, bool hasMinimumToOpenAccount,
            double? minimumToOpenAccount, bool hasMonthlyFee,
            double monthlyFee, double? balanceToAvoidFee, bool hasMinimumAmountForApr,
            double? minimumAmountForApr, bool hasMaximumAmountForApr, 
            double? maximumAmountForApr, double? aprIfAmountNotMet, string name, SavingsAccount account)
        {
            if (ModelState.IsValid)
            {
                account.InterestRate = interestRate;
                account.HasMinimumToOpenAccount = hasMinimumToOpenAccount;
                account.MinimumToOpenAccount = minimumToOpenAccount;
                account.HasMonthlyFee = hasMonthlyFee;
                account.MonthlyFee = monthlyFee;
                account.BalanceToAvoidFee = balanceToAvoidFee;
                account.HasMinimumAmountForApr = hasMinimumAmountForApr;
                account.MinimumAmountForApr = minimumAmountForApr;
                account.HasMaximumAmountForApr = hasMaximumAmountForApr;
                account.MaximumAmountForApr = maximumAmountForApr;
                account.AprIfAmountNotMet = aprIfAmountNotMet;
                account.Name = name;
                // _db.SavingsAccounts.Add(account);
                // _db.SaveChanges();
                var addStatus = _savingsAccountData.AddSavingsAccount(account);
                if(addStatus == 0)
                {
                    // add succeeded
                    var commitStatus = _savingsAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = account.Id });

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

        // GET: SavingsAccounts/Edit/5
        [Authorize(Roles="Administrator")]
        public ActionResult Edit(int id)
        {
            var account = _savingsAccountData.GetById(id);
            if (account != null)
            {
                TempData["AccountId"] = id;
                return View(account);
            }
            else
            {
                // add failed to get details message
                return RedirectToAction("Index");
            }

        }

        // POST: SavingsAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public ActionResult Edit(double interestRate, bool hasMinimumToOpenAccount,
            double? minimumToOpenAccount, bool hasMonthlyFee,
            double monthlyFee, double? balanceToAvoidFee, bool hasMinimumAmountForApr,
            double? minimumAmountForApr, bool hasMaximumAmountForApr,
            double? maximumAmountForApr, double? aprIfAmountNotMet, string name)
        {
            if (ModelState.IsValid)
            {
                var accountId = (int)TempData["AccountId"];
                var account = _savingsAccountData.GetById(accountId);
                if(account != null)
                {
                    account.InterestRate = interestRate;
                    account.HasMinimumToOpenAccount = hasMinimumToOpenAccount;
                    account.MinimumToOpenAccount = minimumToOpenAccount;
                    account.HasMonthlyFee = hasMonthlyFee;
                    account.MonthlyFee = monthlyFee;
                    account.BalanceToAvoidFee = balanceToAvoidFee;
                    account.HasMinimumAmountForApr = hasMinimumAmountForApr;
                    account.MinimumAmountForApr = minimumAmountForApr;
                    account.HasMaximumAmountForApr = hasMaximumAmountForApr;
                    account.MaximumAmountForApr = maximumAmountForApr;
                    account.AprIfAmountNotMet = aprIfAmountNotMet;
                    account.Name = name;
                    var commitStatus = _savingsAccountData.Commit();
                    if(commitStatus == 0)
                    {
                        // commit succeeded
                        return RedirectToAction("Details", new { id = account.Id });
                    }
                    else
                    {
                        // commit failed
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    // account is null
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        // GET: SavingsAccounts/Delete/5
        [Authorize(Roles ="Administrator")]
        public ActionResult Delete(int id)
        {
            var account = _savingsAccountData.GetById(id);
            if(account != null)
            {
                TempData["AccountId"] = account.Id;
                return View(account);
            }
            return RedirectToAction("Index");
           
        }

        // POST: SavingsAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public ActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["AccountId"];
                var account = _savingsAccountData.GetById(id);
                if(account != null)
                {

                    var removeStatus = _savingsAccountData.RemoveSavingsAccount(account);
                    if(removeStatus == 0)
                    {
                        // remove succeeded
                        var commitStatus = _savingsAccountData.Commit();
                        if(commitStatus == 0)
                        {
                            // delete succeeded
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
                // account is null
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}












