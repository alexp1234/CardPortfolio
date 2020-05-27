using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IAutoLoanData
    {
        IEnumerable<AutoLoan> GetAllAutoLoans();
        IEnumerable<AutoLoan> GetUnassignedAutoLoans();
        int Commit();

        AutoLoan GetAutoLoanById(int id);

        int AddAutoLoan(AutoLoan autoLoan);

        int RemoveAutoLoan(AutoLoan autoLoan);



    }
}
