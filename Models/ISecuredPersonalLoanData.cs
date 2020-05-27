using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface ISecuredPersonalLoanData
    {
        IEnumerable<SecuredPersonalLoan> GetAllSecuredPersonalLoans();
        IEnumerable<SecuredPersonalLoan> GetUnassignedPersonalLoans();
        SecuredPersonalLoan GetById(int id);
        int Commit();
        int AddSecuredPersoanlLoan(SecuredPersonalLoan securedPersonalLoan);
        int RemoveSecuredPersonalLoan(SecuredPersonalLoan securedPersonalLoan);
    }
}
