using SADC.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SADC.Domain;

namespace SADC.Persistence.Context
{
    public class SADCContext : IdentityDbContext<User, Role, int,
                                                        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public SADCContext(DbContextOptions<SADCContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Planting> Plantings { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Seed> Seeds { get; set; }
        public DbSet<PlantingField> PlantingFields { get; set; }
        public DbSet<EmployeesFarms> EmployeesFarms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasOne(ur => ur.Role)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();

                userRole.HasOne(ur => ur.User)
                        .WithMany(r => r.UserRoles)
                        .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<PlantingField>()
                        .HasKey(PF => new { PF.PlantingId, PF.FieldId });

            modelBuilder.Entity<EmployeesFarms>()
                        .HasKey(EF => new { EF.FarmId, EF.EmployeeId });


            modelBuilder.Entity<Farm>()
                .HasMany(f => f.Fields)
                .WithOne(p => p.Farm)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
