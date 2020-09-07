using HRA.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HRA.DAL
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public EFUnitOfWork(DbContext context)
        {
            Context = context;
        }

        public DbContext Context { get; private set; }

        public ITransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Snapshot)
        {
            return new DbTransaction(Context.Database.BeginTransaction(isolationLevel));
        }

        public void Add<T>(T obj)
            where T : class
        {
            var entity = obj as BaseEntity;
            entity.CreatedDate = DateTime.UtcNow;
            entity.ModifiedDate = DateTime.UtcNow;
            var set = Context.Set<T>();
            set.Add(obj);
        }

        public void Update<T>(T obj)
            where T : class
        {
            var entity = obj as BaseEntity;
            entity.ModifiedDate = DateTime.UtcNow; 
            var set = Context.Set<T>();
            set.Attach(obj);
            Context.Entry(obj).State = EntityState.Modified;
        }

        void IUnitOfWork.Remove<T>(T obj)
        {
            var set = Context.Set<T>();
            set.Remove(obj);
        }

        public IQueryable<T> Query<T>()
            where T : class
        {
            return Context.Set<T>().AsNoTracking();
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Attach<T>(T newUser) where T : class
        {
            var set = Context.Set<T>();
            set.Attach(newUser);
        }

        public void Dispose()
        {
            Context = null;
        }
    }
}
