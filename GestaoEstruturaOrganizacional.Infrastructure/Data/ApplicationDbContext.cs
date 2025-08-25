using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using GestaoEstruturaOrganizacional.Core.Entities;

namespace GestaoEstruturaOrganizacional.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SIT_DB;Username=postgres;Password=#abc123#");
            }
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<EPI> EPIs { get; set; }
        public DbSet<Risk> Risks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Company configuration
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CNPJ).HasMaxLength(18);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.ContactPerson).HasMaxLength(255);
                entity.Property(e => e.ContactEmail).HasMaxLength(255);
                entity.Property(e => e.ContactPhone).HasMaxLength(20);
            });

            // Unit configuration
            modelBuilder.Entity<Unit>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Address).HasMaxLength(255);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.State).HasMaxLength(2);
                entity.Property(e => e.ZipCode).HasMaxLength(10);

                entity.HasOne(e => e.Company)
                      .WithMany(c => c.Units)
                      .HasForeignKey(e => e.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Area configuration
            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.HasOne(e => e.Unit)
                      .WithMany(u => u.Areas)
                      .HasForeignKey(e => e.UnitId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Sector configuration
            modelBuilder.Entity<Sector>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.HasOne(e => e.Area)
                      .WithMany(a => a.Sectors)
                      .HasForeignKey(e => e.AreaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.HasOne(e => e.Sector)
                      .WithMany(s => s.Roles)
                      .HasForeignKey(e => e.SectorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // JobPosition configuration
            modelBuilder.Entity<JobPosition>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CBOCode).HasMaxLength(20);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.EducationLevel).HasMaxLength(100);
                entity.Property(e => e.Experience).HasMaxLength(255);
                entity.Property(e => e.Skills).HasMaxLength(1000);
                entity.Property(e => e.Competencies).HasMaxLength(1000);

                entity.HasOne(e => e.Role)
                      .WithMany(r => r.JobPositions)
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.ReportingTo)
                      .WithMany()
                      .HasForeignKey(e => e.ReportingToId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // EPI configuration
            modelBuilder.Entity<EPI>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Type).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.CANumber).HasMaxLength(50);

                entity.HasOne(e => e.JobPosition)
                      .WithMany(jp => jp.EPIs)
                      .HasForeignKey(e => e.JobPositionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Risk configuration
            modelBuilder.Entity<Risk>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Severity).HasMaxLength(50);
                entity.Property(e => e.ControlMeasures).HasMaxLength(1000);

                entity.HasOne(e => e.JobPosition)
                      .WithMany(jp => jp.Risks)
                      .HasForeignKey(e => e.JobPositionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
