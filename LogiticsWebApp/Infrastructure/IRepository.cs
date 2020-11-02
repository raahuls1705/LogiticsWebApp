using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LogiticsWebApp.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Delete(T entityToDelete);
        void Delete(object id);
        void DeleteRange(IEnumerable<T> range);
        IEnumerable<T> Get(
            ref int totalCounts,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int currentPage = 1,
            int rows = 10);
        IEnumerable<T> GetAll();
        T GetByID(object id);
        IEnumerable<T> GetWithRawSql(string query,
            params object[] parameters);
        void Insert(T entity);
        void InsertRange(IEnumerable<T> range);
        void Update(T entityToUpdate);
        void Commit();
    }
}
