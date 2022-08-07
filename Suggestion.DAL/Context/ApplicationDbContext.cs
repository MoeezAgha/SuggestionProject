using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Suggestion.BL.Model;

namespace Suggestion.DAL.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>// DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //Add-Migration MigrationName -Project Suggestion.Server
        //protected void OnConfiguring(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<IdentityUserLogin<int>>()
        //   .Property(login => login.UserId)
        //   .ForMySQLHasColumnType("PK")
        //   .UseSqlServerIdentityColumn()
        //   .UseMySQLAutoIncrementColumn("AI");

        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("UserToken");

        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }

        public DbSet<Tweet> Tweet { get; set; }

        //  public DbSet<UserLogin> UserLogins { get; set; }

    }


}
