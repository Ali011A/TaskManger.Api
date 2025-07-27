using Microsoft.EntityFrameworkCore;
using TaskManger.Api.Entities;

namespace TaskManger.Api.Data
{
    public class TaskMangerDbContext(DbContextOptions options) : DbContext(options)
    {

        public DbSet<Tasks>? Tasks { get; set; }
        public DbSet<Member>? Members { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tasks>().ToTable("Tasks");
            modelBuilder.Entity<Member>().ToTable("Members");
        }
    }
}
