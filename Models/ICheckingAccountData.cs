using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface ICheckingAccountData
    {
        IEnumerable<CheckingAccount> GetAllCheckingAccounts();
        IEnumerable<CheckingAccount> GetUnassignedCheckingAccounts();
        CheckingAccount GetById(int id);
        int Commit();
        int AddCheckingAccount(CheckingAccount checkingAccount);
        int RemoveCheckingAccount(CheckingAccount checkingAccount);

        
    }
}
