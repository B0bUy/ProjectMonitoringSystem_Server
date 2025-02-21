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
        public DbSet<Entities.Models.TaskStatus> TaskStatus { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public DbSet<ItemHistory> ItemHistories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientInclusion> ClientInclusions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Access> Access { get; set; }
        public DbSet<UserAccess> UserAccess { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Access>().HasData(
                new Access
                {
                    AccessId = new Guid("e0684bf4-d91b-4099-ac68-0bdbb9bc0a50"),
                    Name = "Read"
                },
                new Access
                {
                    AccessId = new Guid("6141b753-bfc8-4af2-8912-4b2828459c2b"),
                    Name = "User"
                },
                new Access
                {
                    AccessId = new Guid("eb6afbf8-63b0-46bd-bb21-ffec9f890fec"),
                    Name = "Encoder"
                },
                new Access
                {
                    AccessId = new Guid("37426077-ccdf-4043-b7f1-5aef998f1a45"),
                    Name = "Contributor"
                },
                new Access
                {
                    AccessId = new Guid("14772961-c4fc-4dcd-9a78-ef20ad55087f"),
                    Name = "Admin"
                }
            );
        }
    }
}
