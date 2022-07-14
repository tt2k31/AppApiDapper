using Microsoft.EntityFrameworkCore;

namespace AppApiDapper.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        public DbSet<AspnetMembership> AspnetMemberships { get; set; } = null!;
        public DbSet<AspnetOrganization> AspnetOrganizations { get; set; } = null!;
        public DbSet<AspnetUser> AspnetUsers { get; set; } = null!;
        public DbSet<ManagerList> ManagerLists { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AspnetMembership>(e =>
            {
                e.HasKey(e => e.UserId);
                e.ToTable("aspnet_Membership");

                e.Property(e => e.UserId).ValueGeneratedNever();

                e.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                e.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                e.Property(e => e.FullName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                e.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ManagerList>(e =>
            {
                e.HasKey(e => new { e.UserId, e.OrganizationId });

                e.ToTable("aspnet_ManagerList");

                e.HasOne(e => e.AspnetUser)
                    .WithMany(e => e.ManagerLists)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_ML_User");

                e.HasOne(e => e.AspnetOrganization)
                    .WithMany(e => e.ManagerLists)
                    .HasForeignKey(e => e.OrganizationId)
                    .HasConstraintName("FK_ML_Organ");
            });
            
            modelBuilder.Entity<AspnetUser>(e =>
            {
                e.ToTable("aspnet_User");

                e.HasKey(e => e.UserId);

                e.Property(e => e.UserId).ValueGeneratedNever();

                e.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspnetOrganization>(e =>
            {
                e.ToTable("aspnet_Organization");
                e.HasKey(e => e.OrganizationId);

                e.Property(e => e.OrganizationId).ValueGeneratedNever();

                e.Property(e => e.Description)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                e.Property(e => e.OrganizationCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                e.Property(e => e.OrganizationName)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                e.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                
            });
        }
    }
}
