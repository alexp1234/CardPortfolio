using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class UnsecuredPersonalLoanData : IUnsecuredPersonalLoanData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public UnsecuredPersonalLoanData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddUnsecuredPersonalLoan(UnsecuredPersonalLoan unsecuredPersonalLoan)
        {
            try
            {
                _db.UnsecuredPersonalLoans.Add(unsecuredPersonalLoan);
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

        public int DeleteUnsecuredPersonalLoan(UnsecuredPersonalLoan unsecuredPersonalLoan)
        {
            try
            {
                _db.UnsecuredPersonalLoans.Remove(unsecuredPersonalLoan);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public IEnumerable<UnsecuredPersonalLoan> GetAllUnsecuredPersonalLoans()
        {
            return _db.UnsecuredPersonalLoans.ToList();
        }

        public UnsecuredPersonalLoan GetById(int id)
        {
            var loan = _db.UnsecuredPersonalLoans.Find(id);
            return loan;
        }

        public IEnumerable<UnsecuredPersonalLoan> GetUnassignedPersonalLoans()
        {
            return _db.UnsecuredPersonalLoans.Where(u => u.InstitutionId == null).ToList();
        }
    }
}
