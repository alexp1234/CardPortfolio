using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class SavingsAccountData : ISavingsAccountData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public SavingsAccountData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddSavingsAccount(SavingsAccount savingsAccount)
        {
            try
            {
                _db.SavingsAccounts.Add(savingsAccount);
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

        public IEnumerable<SavingsAccount> GetAllSavingsAccounts()
        {
            return _db.SavingsAccounts.ToList();
        }

        public IEnumerable<SavingsAccount> GetUnassignedSavingsAccounts()
        {
            return _db.SavingsAccounts.Where(s => s.InstitutionId == null).ToList();
        }

        public int RemoveSavingsAccount(SavingsAccount savingsAccount)
        {
            try
            {
                _db.SavingsAccounts.Remove(savingsAccount);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public SavingsAccount GetById(int id)
        {
            var account = _db.SavingsAccounts.Find(id);
            return account;
        }
    }
}
