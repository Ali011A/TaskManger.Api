using TaskManger.Api.Entities;

namespace TaskManger.Api.Interfaces
{
    public interface IUnitOfWork
    {


        void Commit();
        Task CommitAsync();

        IRepository<Tasks> Tasks { get; }
        IRepository<Member> Members { get; }
    }
}
