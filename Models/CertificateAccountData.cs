using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class CertificateAccountData : ICertificateAccountData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public CertificateAccountData( ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;

        }

        public int AddCertificateAccount(CertificateAccount certificateAccount)
        {
            try
            {
                _db.CertificateAccounts.Add(certificateAccount);
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

        public IEnumerable<CertificateAccount> GetAllCertificateAccounts()
        {
            return _db.CertificateAccounts.ToList();
        }

        public CertificateAccount GetById(int id)
        {
            var account = _db.CertificateAccounts.Find(id);
            return account;
        }

        public IEnumerable<CertificateAccount> GetUnassignedCertificateAccounts()
        {
           return  _db.CertificateAccounts.Where(a => a.InstitutionId == null).ToList();
        }

        public int RemoveCertificateAccount(CertificateAccount certificateAccount)
        {
            try
            {
                _db.CertificateAccounts.Remove(certificateAccount);
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
