using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class MoneyMarketAccountData : IMoneyMarketAccountData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public MoneyMarketAccountData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddMoneyMarketAccount(MoneyMarketAccount moneyMarketAccount)
        {
            try
            {
                _db.MoneyMarketAccounts.Add(moneyMarketAccount);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public int Commit()
        {
            try
            {
                _db.SaveChanges();
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public IEnumerable<MoneyMarketAccount> GetAllMoneyMarketAccounts()
        {
            return _db.MoneyMarketAccounts.ToList();
        }

        public MoneyMarketAccount GetById(int id)
        {
            var account = _db.MoneyMarketAccounts.Find(id);
            return account;
        }

        public IEnumerable<MoneyMarketAccount> GetUnassignedMoneyMarketAccounts()
        {
            return _db.MoneyMarketAccounts.Where(m => m.InstitutionId == null).ToList();
        }

        public int RemoveMoneyMarketAccount(MoneyMarketAccount moneyMarketAccount)
        {
            try
            {
                _db.MoneyMarketAccounts.Remove(moneyMarketAccount);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }
    }
}
