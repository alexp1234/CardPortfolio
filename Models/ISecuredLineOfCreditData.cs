using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface ISecuredLineOfCreditData
    {
        IEnumerable<SecuredLineOfCredit> GetAllSecuredLinesOfCredit();
        IEnumerable<SecuredLineOfCredit> GetUnassignedSecuredLinesOfCredit();
        SecuredLineOfCredit GetById(int id);
        int Commit();
        int RemoveSecuredLineOfCredit(SecuredLineOfCredit securedLineOfCredit);
        int AddSecuredLineOfCredit(SecuredLineOfCredit securedLineOfCredit);
        
    }
}
