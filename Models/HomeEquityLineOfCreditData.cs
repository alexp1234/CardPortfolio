using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class HomeEquityLineOfCreditData: IHomeEquityLineOfCreditData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public HomeEquityLineOfCreditData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        public int AddHomeEquityLineOfCredit(HomeEquityLineOfCredit homeEquityLineOfCredit)
        {
            try
            {
                _db.HomeEquityLinesOfCredit.Add(homeEquityLineOfCredit);
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

        public IEnumerable<HomeEquityLineOfCredit> GetAllHomeEquityLinesOfCredit()
        {
            return _db.HomeEquityLinesOfCredit.ToList();

        }

        public HomeEquityLineOfCredit GetById(int id)
        {
            var heloc = _db.HomeEquityLinesOfCredit.Find(id);
            return heloc;
        }

        public IEnumerable<HomeEquityLineOfCredit> GetUnassignedHomeEquityLinesOfCredit()
        {
            return _db.HomeEquityLinesOfCredit.Where(h => h.InstitutionId == null).ToList();
        }

        public int RemoveHomeEquityLineOfCredit(HomeEquityLineOfCredit homeEquityLineOfCredit)
        {
            try
            {
                _db.HomeEquityLinesOfCredit.Remove(homeEquityLineOfCredit);
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
