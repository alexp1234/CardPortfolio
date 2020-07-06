using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using CardPortfolio.Models.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class InstitutionData: IInstitutionData
    {
        private readonly ApplicationDbContext _db;
        private readonly ILogger<IndexModel> _logger;
        public InstitutionData(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _logger = logger;
        }

        public Institution AddCreditCardByInstitutionId(int id, CreditCard creditCard)
        {
            var institution = _db.Institutions.Find(id);
            if (institution != null)
            {
                _db.CreditCards.Add(creditCard);
                institution.CreditCards.Add(creditCard);
                _db.SaveChanges();
            }
            return institution;
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

        public int Create(Institution institution)
        {
            try
            {
                _db.Add(institution);
                return 0;

            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }

        }

        public IEnumerable<Institution> CreditUnionsAnyoneCanJoin()
        {
            return _db.Institutions.Where(i => i.InstitutionType == InstitutionType.CreditUnion && i.HasRestrictionsToJoin == false).ToList();
        }

        public int Delete(Institution institution)
        {
            try
            {
                _db.Institutions.Remove(institution);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public Institution Edit(Institution updatedInstitution)
        {
            var institution = _db.Institutions.SingleOrDefault(i => i.Id == updatedInstitution.Id);
            if (institution != null)
            {
                institution.InstitutionType = updatedInstitution.InstitutionType;
                institution.Name = updatedInstitution.Name;

            }
            return institution;
        }

        public IEnumerable<Institution> GetAll()
        {
            return _db.Institutions.ToList();
        }

        public IEnumerable<Institution> GetAllBanks()
        {
            return _db.Institutions.Where(i => i.InstitutionType == InstitutionType.Bank).ToList();
        }

        public IEnumerable<Institution> GetAllCreditUnions()
        {
            return _db.Institutions.Where(i => i.InstitutionType == InstitutionType.CreditUnion).ToList();
        }

        public Institution GetById(int id)
        {
            var institution = _db.Institutions.Find(id);
            return institution;
        }

        public int GetCountOfInstitutions()
        {
            return _db.Institutions.Count();

        }


        public IEnumerable<Institution> GetInstitutionsByInstitutionType(InstitutionType type)
        {
            return _db.Institutions.Where(i => i.InstitutionType == type).ToList();
        }


        public IEnumerable<CreditCard> GetInstitutionsCreditCards(int id)
        {
          
            return _db.CreditCards.Where(c => c.InstitutionId == id).ToList();
        }

    }
}

