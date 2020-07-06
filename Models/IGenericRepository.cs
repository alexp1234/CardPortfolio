using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public interface IGenericRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        int Insert(T obj);
        int Delete(T obj);
        int Commit();

        
    }
}
