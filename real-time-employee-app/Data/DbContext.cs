using Microsoft.EntityFrameworkCore;
using real_time_employee_app.Models;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Notification> Notification { get; set; }

}
