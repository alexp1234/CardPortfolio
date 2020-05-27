using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IMoneyMarketAccountData
    {
        int Commit();
        int AddMoneyMarketAccount(MoneyMarketAccount moneyMarketAccount);
        int RemoveMoneyMarketAccount(MoneyMarketAccount moneyMarketAccount);
        IEnumerable<MoneyMarketAccount> GetAllMoneyMarketAccounts();
        IEnumerable<MoneyMarketAccount> GetUnassignedMoneyMarketAccounts();
        MoneyMarketAccount GetById(int id);

    }
}
