using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class CheckingAccountData: ICheckingAccountData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public CheckingAccountData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;

        }

        public int AddCheckingAccount(CheckingAccount checkingAccount)
        {
            try
            {
                _db.CheckingAccounts.Add(checkingAccount);
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

        public IEnumerable<CheckingAccount> GetAllCheckingAccounts()
        {
            return _db.CheckingAccounts.ToList();
        }

        public CheckingAccount GetById(int id)
        {
            var account = _db.CheckingAccounts.Find(id);
            return account;
        }

        public IEnumerable<CheckingAccount> GetUnassignedCheckingAccounts()
        {
            return _db.CheckingAccounts.Where(c => c.InstitutionId == null).ToList();
        }

        public int RemoveCheckingAccount(CheckingAccount checkingAccount)
        {
            try
            {
                _db.CheckingAccounts.Remove(checkingAccount);
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
