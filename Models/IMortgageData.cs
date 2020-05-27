using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IMortgageData
    {
        IEnumerable<Mortgage> GetAllMortgages();
        IEnumerable<Mortgage> GetUnassignedMortgages();
        Mortgage GetById(int id);
        int Commit();
        int RemoveMortage(Mortgage mortgage);
        int AddMortgage(Mortgage mortgage);
    }
}
