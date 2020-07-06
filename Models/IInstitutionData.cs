using CardPortfolio.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IInstitutionData
    {
        Institution GetById(int id);
        IEnumerable<Institution> GetInstitutionsByInstitutionType(InstitutionType type);
        IEnumerable<Institution> GetAll();
        IEnumerable<Institution> GetAllBanks();
        IEnumerable<Institution> GetAllCreditUnions();

        IEnumerable<Institution> CreditUnionsAnyoneCanJoin();

        Institution Edit(Institution institution);
        int Create(Institution institution);
        int Delete(Institution institution);
        int GetCountOfInstitutions();
        Institution AddCreditCardByInstitutionId(int id, CreditCard creditCard);

        IEnumerable<CreditCard> GetInstitutionsCreditCards(int id);
        int Commit();
    }
}
