using LogiticsWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LogiticsWebApp.Infrastructure
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        internal LogisticDbContext context;
        internal DbSet<T> dbSet;

        public BaseRepository(LogisticDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public virtual IEnumerable<T> GetWithRawSql(string query,
            params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters).ToList();
        }

        public virtual IEnumerable<T> Get(
            ref int totalCounts,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int currentPage = 1,
            int rows = 10)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            double pageCount = (double)((decimal)query.Count() / Convert.ToDecimal(rows));
            totalCounts = (int)Math.Ceiling(pageCount);

            query = query.Skip((currentPage - 1) * rows).Take(rows);

            if (orderBy != null)
                return orderBy(query).ToList();
            else return query.ToList();
        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void DeleteRange(IEnumerable<T> range)
        {
            dbSet.RemoveRange(range);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public void InsertRange(IEnumerable<T> range)
        {
            dbSet.AddRange(range);
        }

        public void Commit()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }


    }
}
