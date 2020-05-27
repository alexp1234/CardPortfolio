using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class MortgageData : IMortgageData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public MortgageData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }
        public int AddMortgage(Mortgage mortgage)
        {
            try
            {
                _db.Mortgages.Add(mortgage);
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
                return 0;
            }
        }

        public IEnumerable<Mortgage> GetAllMortgages()
        {
            return _db.Mortgages.ToList();
        }

        public Mortgage GetById(int id)
        {
            var mortgage = _db.Mortgages.Find(id);
            return mortgage;
        }

        public IEnumerable<Mortgage> GetUnassignedMortgages()
        {
            return _db.Mortgages.Where(m => m.InstitutionId == null).ToList();
        }

        public int RemoveMortage(Mortgage mortgage)
        {
            try
            {
                _db.Mortgages.Remove(mortgage);
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
