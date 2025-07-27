using Microsoft.EntityFrameworkCore;
using TaskManger.Api.Entities;
using TaskManger.Api.Interfaces;

namespace TaskManger.Api.Data.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TaskMangerDbContext _context;
        DbSet<T> Set => _context.Set<T>();
        public GenericRepository(TaskMangerDbContext context)
        {
            _context = context;

        }
        public async Task AddAsync(T entity)
        {


            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            await Set.AddAsync(entity);
          


        }

        public void Delete(int id)
        {

            //soft delete
            var entity = Set.Find(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity with id {id} not found");

            entity.IsDeleted = true;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string includeProperties = "", int pageNo = 1, int pageSize = 10)
        {

            int skip = (pageNo - 1) * pageSize;
            IQueryable<T> query = Set.Where(x => !x.IsDeleted);

            foreach (var includeProp in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }

            return await query.Skip(skip).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {


            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null || entity.IsDeleted)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found or has been deleted");
            }
            return entity;
        }

        public Task SaveAsync()
        {

            return _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            var query = Set.Where<T>(x => !x.IsDeleted);

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
            }
            if (!query.Any(x => x.Id == entity.Id))
            {
                throw new KeyNotFoundException($"Entity with id {entity.Id} not found");
            }

            _context.Set<T>().Update(entity);
            
        }
    }
}
