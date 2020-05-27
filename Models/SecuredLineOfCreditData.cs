using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class SecuredLineOfCreditData : ISecuredLineOfCreditData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public SecuredLineOfCreditData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddSecuredLineOfCredit(SecuredLineOfCredit securedLineOfCredit)
        {
            try
            {
                _db.SecuredLinesOfCredit.Add(securedLineOfCredit);
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

        public IEnumerable<SecuredLineOfCredit> GetAllSecuredLinesOfCredit()
        {
            return _db.SecuredLinesOfCredit.ToList();
        }

        public SecuredLineOfCredit GetById(int id)
        {
            var loc = _db.SecuredLinesOfCredit.Find(id);
            return loc;
        }

        public IEnumerable<SecuredLineOfCredit> GetUnassignedSecuredLinesOfCredit()
        {
            return _db.SecuredLinesOfCredit.Where(s => s.InstitutionId == null).ToList();
        }

        public int RemoveSecuredLineOfCredit(SecuredLineOfCredit securedLineOfCredit)
        {
            try
            {
                _db.SecuredLinesOfCredit.Remove(securedLineOfCredit);
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
