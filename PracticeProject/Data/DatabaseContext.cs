using Microsoft.EntityFrameworkCore;
using PracticeProject.Model;

namespace PracticeProject.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions options) : base(options) { }


        public DbSet<Employee> Employee { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<Customer> Customers { get; set; }


    }
}
