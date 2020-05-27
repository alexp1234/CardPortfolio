using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IHomeEquityLineOfCreditData
    {
        int AddHomeEquityLineOfCredit(HomeEquityLineOfCredit homeEquityLineOfCredit);
        int RemoveHomeEquityLineOfCredit(HomeEquityLineOfCredit homeEquityLineOfCredit);
        int Commit();
        IEnumerable<HomeEquityLineOfCredit> GetAllHomeEquityLinesOfCredit();
        IEnumerable<HomeEquityLineOfCredit> GetUnassignedHomeEquityLinesOfCredit();
        HomeEquityLineOfCredit GetById(int id);

    }
}
