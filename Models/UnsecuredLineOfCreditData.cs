using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class UnsecuredLineOfCreditData : IUnsecuredLineOfCreditData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;

        public UnsecuredLineOfCreditData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddUnsecuredLineOfCredit(UnsecuredLineOfCredit unsecuredLineOfCredit)
        {
            try
            {
                _db.UnsecuredLinesOfCredit.Add(unsecuredLineOfCredit);
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

        public IEnumerable<UnsecuredLineOfCredit> GetAllUnsecuredLinesOfCredit()
        {
            return _db.UnsecuredLinesOfCredit.ToList();
        }

        public UnsecuredLineOfCredit GetById(int id)
        {
            var loc = _db.UnsecuredLinesOfCredit.Find(id);
            return loc;
        }

        public IEnumerable<UnsecuredLineOfCredit> GetUnassignedUnsecuredLinesOfCredit()
        {
            return _db.UnsecuredLinesOfCredit.Where(u => u.InstitutionId == null).ToList();
        }

        public int RemoveUnsecuredLineOfCredit(UnsecuredLineOfCredit unsecuredLineOfCredit)
        {
            try
            {
                _db.UnsecuredLinesOfCredit.Remove(unsecuredLineOfCredit);
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
