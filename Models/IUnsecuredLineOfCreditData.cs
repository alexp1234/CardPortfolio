using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IUnsecuredLineOfCreditData
    {
        IEnumerable<UnsecuredLineOfCredit> GetAllUnsecuredLinesOfCredit();
        IEnumerable<UnsecuredLineOfCredit> GetUnassignedUnsecuredLinesOfCredit();
        UnsecuredLineOfCredit GetById(int id);
        int Commit();
        int RemoveUnsecuredLineOfCredit(UnsecuredLineOfCredit unsecuredLineOfCredit);
        int AddUnsecuredLineOfCredit(UnsecuredLineOfCredit unsecuredLineOfCredit);
    }
}
