using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IUnsecuredPersonalLoanData
    {
        IEnumerable<UnsecuredPersonalLoan> GetAllUnsecuredPersonalLoans();
        IEnumerable<UnsecuredPersonalLoan> GetUnassignedPersonalLoans();
        UnsecuredPersonalLoan GetById(int id);
        int Commit();
        int DeleteUnsecuredPersonalLoan(UnsecuredPersonalLoan unsecuredPersonalLoan);
        int AddUnsecuredPersonalLoan(UnsecuredPersonalLoan unsecuredPersonalLoan);
    }
}
