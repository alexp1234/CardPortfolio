using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IHomeEquityLoanData
    {
        int Commit();
        int AddHomeEquityLoan(HomeEquityLoan homeEquityLoan);
        int RemoveHomeEquityLoan(HomeEquityLoan homeEquityLoan);
        IEnumerable<HomeEquityLoan> GetAllHomeEquityLoans();
        IEnumerable<HomeEquityLoan> GetUnassignedHomeEquityLoans();
        HomeEquityLoan GetById(int id);

    }
}
