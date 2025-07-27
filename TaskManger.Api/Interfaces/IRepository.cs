using TaskManger.Api.Entities;

namespace TaskManger.Api.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {

        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = "",int pageNo = 1, int pageSize = 10);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(int id);
        Task SaveAsync();





    }
}
