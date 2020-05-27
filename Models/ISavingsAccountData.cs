using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface ISavingsAccountData
    {
        int Commit();
        IEnumerable<SavingsAccount> GetAllSavingsAccounts();
        IEnumerable<SavingsAccount> GetUnassignedSavingsAccounts();
        int AddSavingsAccount(SavingsAccount savingsAccount);
        int RemoveSavingsAccount(SavingsAccount savingsAccount);
        SavingsAccount GetById(int id);

    }
}
