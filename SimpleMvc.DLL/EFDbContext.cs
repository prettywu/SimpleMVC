using SimpleMvc.Entitys;
using System.Data.Entity;

namespace SimpleMvc.DAL
{
    public class EFDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Lawsuit> Lawsuits { get; set; }
        public DbSet<Opinion> Opinions { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
