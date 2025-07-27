using TaskManger.Api.Entities;
using TaskManger.Api.Interfaces;

namespace TaskManger.Api.Data.Repository
{
    public class UnitofWork : IUnitOfWork
    {
        private readonly TaskMangerDbContext _context;
        private readonly IRepository<Tasks> _tasksRepository;
        private readonly IRepository<Member> _membersRepository;
        public UnitofWork(TaskMangerDbContext context, IRepository<Tasks> tasksRepository,
            IRepository<Member> membersRepository)
        {
            _context = context ;
            _tasksRepository = tasksRepository;
            _membersRepository = membersRepository;
        }
        public IRepository<Tasks> Tasks => _tasksRepository;

        public IRepository<Member> Members => _membersRepository;

        public void Commit()
        {

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while committing changes.", ex);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while committing changes asynchronously.", ex);
            }

        }

       
    }
}
