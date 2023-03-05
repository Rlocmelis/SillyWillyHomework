namespace SillyWillyHomework.Services.BaseService
{
    public interface IBaseService<TEntity, TModel> where TEntity : class where TModel : class
    {
        Task<TModel> GetByIdAsync(int id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<TModel> AddAsync(TModel model);
        Task UpdateAsync(int id, TModel model);
        Task DeleteAsync(int id);
    }
}
