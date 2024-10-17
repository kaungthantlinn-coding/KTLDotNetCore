using Microsoft.EntityFrameworkCore;

namespace ToDoListApi.DataModels
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Data Source=.;Initial Catalog=ToDoList;User ID=sa;Password=sasa@123;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<ToDoList> ToDoLists { get; set; }
        public DbSet<TaskCategory> TaskCategories { get; set; }


    }
}
