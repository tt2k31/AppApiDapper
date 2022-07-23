namespace AppApiDapper.Services.Interface
{
    public interface IGenericRepository <T> where T : class
    {
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid id);
        Task Add(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
    }
}
