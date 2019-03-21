using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DevConfSkopje.Data.Repository
{
    public class Repository<T> where T : class
    {
        protected DbContext Context { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public Repository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected DbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }

        protected IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        protected virtual IQueryable<T> Include(Expression<Func<T, object>> include)
        {
            return this.DbSet.Include(include).AsQueryable();
        }

        protected virtual T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        protected virtual T GetByKey(object[] key)
        {
            return this.DbSet.Find(key);
        }

        protected virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.FirstOrDefault(predicate);
        }

        protected virtual T Add(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                entity = this.DbSet.Add(entity);
            }

            return entity;
        }

        protected virtual void Update(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        protected virtual void Delete(T entity)
        {
            DbEntityEntry entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        protected virtual void DeleteById(object id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        protected virtual void DeleteByKey(object[] key)
        {
            var entity = this.GetByKey(key);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        protected virtual void DeleteRange(IEnumerable<T> entities)
        {
            this.Context.Set<T>().RemoveRange(entities);
        }

        protected int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        protected void Dispose()
        {
            this.Context.Dispose();
        }
    }

}
