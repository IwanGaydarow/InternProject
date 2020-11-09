namespace HCMS.Data.Repository
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();
        IQueryable<TEntity> AllWithDeleted();

        TEntity GetById(params object[] id);
        Task<TEntity> GetByIdAsync(params object[] id);

        TEntity GetByIdWithDeleted(params object[] id);
        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        void Add(TEntity entity);
        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void HardDelete(TEntity entity);

        void Delete(TEntity entity);
        void Undalete(TEntity entity);

        Task<int> SaveChangesAsnyc();
    }
}
