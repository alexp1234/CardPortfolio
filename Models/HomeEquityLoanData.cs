using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class HomeEquityLoanData : IHomeEquityLoanData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public HomeEquityLoanData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddHomeEquityLoan(HomeEquityLoan homeEquityLoan)
        {
            try
            {
                _db.HomeEquityLoans.Add(homeEquityLoan);
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

        public IEnumerable<HomeEquityLoan> GetAllHomeEquityLoans()
        {
            return _db.HomeEquityLoans.ToList();
        }

        public HomeEquityLoan GetById(int id)
        {
            var loan = _db.HomeEquityLoans.Find(id);
            return loan;
        }

        public IEnumerable<HomeEquityLoan> GetUnassignedHomeEquityLoans()
        {
            return _db.HomeEquityLoans.Where(h => h.InstitutionId == null).ToList();
        }

        public int RemoveHomeEquityLoan(HomeEquityLoan homeEquityLoan)
        {
            try
            {
                _db.HomeEquityLoans.Remove(homeEquityLoan);
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
