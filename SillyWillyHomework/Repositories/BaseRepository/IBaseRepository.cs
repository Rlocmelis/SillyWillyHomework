namespace SillyWillyHomework.Repositories.BaseRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
