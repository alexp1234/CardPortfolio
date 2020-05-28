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

        // Finish
        IEnumerable<Institution> CreditUnionsAnyoneCanJoin();

        Institution Edit(Institution institution);
        int Create(Institution institution);
        int Delete(Institution institution);
        int GetCountOfInstitutions();
        Institution AddCreditCardByInstitutionId(int id, CreditCard creditCard);

        IEnumerable<CreditCard> GetInstitutionsCreditCards(int id);
        IEnumerable<AutoLoan> GetInstitutionsAutoLoans(int id);
        IEnumerable<UnsecuredPersonalLoan> GetInstitutionsUnsecuredPersonalLoans(int id);
        IEnumerable<UnsecuredLineOfCredit> GetInstitutionsUnsecuredLinesOfCredit(int id);
        IEnumerable<SecuredPersonalLoan> GetInstitutionsSecuredPersonalLoans(int id);
        IEnumerable<SecuredLineOfCredit> GetInstitutionsSecuredLinesOfCredit(int id);
        IEnumerable<Mortgage> GetInstitutionsMortgages(int id);
        IEnumerable<CertificateAccount> GetInstitutionsCds(int id);
        IEnumerable<CheckingAccount> GetInstitutionsCheckingAccounts(int id);
        IEnumerable<MoneyMarketAccount> GetInstitutionsMoneyMarketAccount(int id);
        IEnumerable<SavingsAccount> GetInstitutionsSavingsAccounts(int id)

        int Commit();
    }
}
