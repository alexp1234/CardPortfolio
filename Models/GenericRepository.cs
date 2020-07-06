using CardPortfolio.Areas.Identity.Pages.Account.Manage;
using CardPortfolio.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardPortfolio.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;      
        private readonly DbSet<T> _table = null;
        private readonly ILogger<IndexModel> _logger;
        public GenericRepository(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;
            _table = _db.Set<T>();
            _logger = logger;
        }
        public int Commit()
        {
            try
            {
                _db.SaveChanges();
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }

        public int Delete(T obj)
        {
           
            if (obj != null)
            {
                try
                {

                    _table.Remove(obj);
                    return 0;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.ToString());
                    return 1;
                }
            }
            return 1;
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }


        public int Insert(T obj)
        {
            try
            {
                _table.Add(obj);
                return 0;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return 1;
            }
        }
    }
}
