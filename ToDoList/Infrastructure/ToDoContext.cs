using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Infrastructure
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<ToDo> ToDoList { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasKey(x => x.Id);
            modelBuilder.Entity<ToDo>().HasData(
                new ToDo { Id = 1, Content = "first task" },
                new ToDo { Id = 2, Content = "second task" });
        }
    }
}
