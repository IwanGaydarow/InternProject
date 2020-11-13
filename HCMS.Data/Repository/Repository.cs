using HCMS.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HCMS.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IDeletableEntity
    {
        public Repository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        private DbSet<TEntity> DbSet { get; set; }

        private ApplicationDbContext Context { get; set; }

        public void Add(TEntity entity) => this.DbSet.Add(entity);

        public Task AddAsync(TEntity entity) => this.DbSet.AddAsync(entity).AsTask();

        public IQueryable<TEntity> All() => this.DbSet.Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted() => this.DbSet;

        public void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }

        public TEntity GetById(params object[] id)
        {
            var getByIdExpresion = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return this.All().FirstOrDefault(getByIdExpresion);
        }

        public Task<TEntity> GetByIdAsync(params object[] id)
        {
            var getByIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return this.All().FirstOrDefaultAsync(getByIdPredicate);
        }

        public TEntity GetByIdWithDeleted(params object[] id)
        {
            var getByIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return this.AllWithDeleted().FirstOrDefault(getByIdPredicate);
        }

        public Task<TEntity> GetByIdWithDeletedAsync(params object[] id)
        {
            var getByIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return this.AllWithDeleted().FirstOrDefaultAsync(getByIdPredicate);
        }

        public void HardDelete(TEntity entity) => this.DbSet.Remove(entity);

        public Task<int> SaveChangesAsnyc() => this.Context.SaveChangesAsync();

        public void Undalete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        public void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Deleted)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }

        private void CheckIdNull(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
        }
    }
}
