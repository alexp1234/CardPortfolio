using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class SecuredPersonalLoanData : ISecuredPersonalLoanData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public SecuredPersonalLoanData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddSecuredPersoanlLoan(SecuredPersonalLoan securedPersonalLoan)
        {
            try
            {
                _db.SecuredPersonalLoans.Add(securedPersonalLoan);
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

        public IEnumerable<SecuredPersonalLoan> GetAllSecuredPersonalLoans()
        {
            return _db.SecuredPersonalLoans.ToList();
        }

        public SecuredPersonalLoan GetById(int id)
        {
            var loan = _db.SecuredPersonalLoans.Find(id);
            return loan;
        }

        public IEnumerable<SecuredPersonalLoan> GetUnassignedPersonalLoans()
        {
            return _db.SecuredPersonalLoans.Where(s => s.InstitutionId == null).ToList();
        }

        public int RemoveSecuredPersonalLoan(SecuredPersonalLoan securedPersonalLoan)
        {
            try
            {
                _db.SecuredPersonalLoans.Remove(securedPersonalLoan);
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
