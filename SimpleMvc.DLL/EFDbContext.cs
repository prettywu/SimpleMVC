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
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Application> Applications { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //多对多关系表UserRoles
            //modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users).Map(m =>
            //{
            //    m.ToTable("UserRoles");
            //    m.MapLeftKey("UserId");
            //    m.MapRightKey("RoleId");
            //});

            base.OnModelCreating(modelBuilder);
        }
    }
}
