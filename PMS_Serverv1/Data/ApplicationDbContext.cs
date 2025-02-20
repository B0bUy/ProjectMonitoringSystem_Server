using Microsoft.EntityFrameworkCore;
using PMS_Serverv1.Entities.Models;
using PMS_Serverv1.Entities.UserManagement;

namespace PMS_Serverv1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProjectStatus> ProjectStatus { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Entities.Models.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
