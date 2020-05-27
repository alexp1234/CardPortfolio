using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class AutoLoanData : IAutoLoanData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public AutoLoanData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
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

        public IEnumerable<AutoLoan> GetAllAutoLoans()
        {
            return _db.AutoLoans.ToList(); ;
        }

        public AutoLoan GetAutoLoanById(int id)
        {
            return _db.AutoLoans.Find(id);
        }

        public int AddAutoLoan(AutoLoan autoLoan)
        {
            try
            {
                _db.AutoLoans.Add(autoLoan);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public int RemoveAutoLoan(AutoLoan autoLoan)
        {
            try
            {
                _db.AutoLoans.Remove(autoLoan);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public IEnumerable<AutoLoan> GetUnassignedAutoLoans()
        {
            return _db.AutoLoans.Where(a => a.InstitutionId == null).ToList();
        }
    }
}
