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
    public class MoneyMarketAccountsController : Controller
    {
        // TODO: Refactor
        private readonly IMoneyMarketAccountData _moneyMarketAccountData;
        public MoneyMarketAccountsController(IMoneyMarketAccountData moneyMarketAccountData)
        {
            _moneyMarketAccountData = moneyMarketAccountData;
        }
        // GET: MoneyMarketAccounts
        public ActionResult Index()
        {
            // TODO: Refactor
            var list = _moneyMarketAccountData.GetAllMoneyMarketAccounts();
            return View(list);
        }


        // GET: MoneyMarketAccounts/Create
        [Authorize(Roles="Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoneyMarketAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public ActionResult Create(double interestRate,
            bool hasMinimumToOpenAccount, double? minimumToOpenAccount,
            bool hasMonthlyFee, double MonthlyFee, double? balanceToAvoidFee,
            bool hasMinimumAmountForApr, double? minimumAmountForApr, 
            bool hasMaximumAmountForApr, double? maximumAmountForApr, double? aprIfAmountNotMet,
            string name, string linkURL, MoneyMarketAccount account)
        {
            if (ModelState.IsValid)
            {
                account.InterestRate = interestRate;
                account.HasMinimumToOpenAccount = hasMinimumToOpenAccount;
                account.MinimumToOpenAccount = minimumToOpenAccount;
                account.HasMonthlyFee = hasMonthlyFee;
                account.MonthlyFee = MonthlyFee;
                account.BalanceToAvoidFee = balanceToAvoidFee;
                account.HasMinimumAmountForApr = hasMinimumAmountForApr;
                account.MinimumAmountForApr = minimumAmountForApr;
                account.HasMaximumAmountForApr = hasMaximumAmountForApr;
                account.MaximumAmountForApr = maximumAmountForApr;
                account.AprIfAmountNotMet = aprIfAmountNotMet;
                account.Name = name;
                account.LinkURL = linkURL;
                var addStatus = _moneyMarketAccountData.AddMoneyMarketAccount(account);
                if(addStatus == 0)
                {
                    // add succeeded
                    var commitStatus = _moneyMarketAccountData.Commit();
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
                    // add failed
                    return RedirectToAction("Index");
                }

            }
            return View();
            

        }

        // GET: MoneyMarketAccounts/Edit/5
        [Authorize(Roles ="Administrator")]
        public ActionResult Edit(int id)
        {
            var account = _moneyMarketAccountData.GetById(id);
            if (account != null)
            {
                TempData["AccountId"] = id;
                return View(account);
            }
            // TODO: Replace with something went wrong page
            return RedirectToAction("Index");
        }

        // POST: MoneyMarketAccounts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public ActionResult Edit(double interestRate,
            bool hasMinimumToOpenAccount, double? minimumToOpenAccount,
            bool hasMonthlyFee, double MonthlyFee, double? balanceToAvoidFee,
            bool hasMinimumAmountForApr, double? minimumAmountForApr,
            bool hasMaximumAmountForApr, double? maximumAmountForApr,
            double? aprIfAmountNotMet, string name, string linkURL)
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["AccountId"];
                var account = _moneyMarketAccountData.GetById(id);
                if (account != null)
                {
                    account.InterestRate = interestRate;
                    account.HasMinimumToOpenAccount = hasMinimumToOpenAccount;
                    account.MinimumToOpenAccount = minimumToOpenAccount;
                    account.HasMonthlyFee = hasMonthlyFee;
                    account.MonthlyFee = MonthlyFee;
                    account.BalanceToAvoidFee = balanceToAvoidFee;
                    account.HasMinimumAmountForApr = hasMinimumAmountForApr;
                    account.MinimumAmountForApr = minimumAmountForApr;
                    account.HasMaximumAmountForApr = hasMaximumAmountForApr;
                    account.MaximumAmountForApr = maximumAmountForApr;
                    account.AprIfAmountNotMet = aprIfAmountNotMet;
                    account.Name = name;
                    account.LinkURL = linkURL;
                    var commitStatus = _moneyMarketAccountData.Commit();
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
            }
            return View();
        }

        // GET: MoneyMarketAccounts/Delete/5
        [Authorize(Roles ="Administrator")]
        public ActionResult Delete(int id)
        {
            var account = _moneyMarketAccountData.GetById(id);
            if(account != null)
            {
                TempData["AccountId"] = account.Id;
                return View(account);
            }
            return RedirectToAction("Index");
            
        }

        // POST: MoneyMarketAccounts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Administrator")]
        public ActionResult Delete()
        {
            if (ModelState.IsValid)
            {
                var id = (int)TempData["AccountId"];
                var account = _moneyMarketAccountData.GetById(id);
                if(account != null)
                {
                    var removeStatus = _moneyMarketAccountData.RemoveMoneyMarketAccount(account);
                    if(removeStatus == 0)
                    {
                        // remove succeeded
                        var commitStatus = _moneyMarketAccountData.Commit();
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
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}