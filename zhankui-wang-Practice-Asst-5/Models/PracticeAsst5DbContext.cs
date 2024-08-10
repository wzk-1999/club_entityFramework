using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace zhankui_wang_Practice_Asst_5.Models
{
    public partial class PracticeAsst5DbContext : DbContext
    {
        public PracticeAsst5DbContext()
        {
        }

        public PracticeAsst5DbContext(DbContextOptions<PracticeAsst5DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public DbSet<BridgeClub> Clubs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Server=VOCBook15\\SQLEXPRESS;Database=PracticeAsst5DB;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Name).HasName("PK__Cities__737584F6A3497E13");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(e => e.ProvCode)
                    .HasMaxLength(2)
                    .IsRequired();

                entity.HasOne(d => d.ProvCodeNavigation)
                    .WithMany(p => p.Cities)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.ProvCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cities_Provinces");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Province__3214EC2736CD3DBF");

                entity.HasIndex(e => e.Code).IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Code).HasMaxLength(2).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC275723A073");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Address).HasMaxLength(100);
                entity.Property(e => e.CityName).HasMaxLength(30).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.PostalCode).HasMaxLength(6).IsRequired();

                entity.HasOne(d => d.CityNameNavigation)
                    .WithMany(p => p.Users)
                    .HasPrincipalKey(p => p.Name)  // Matches the primary key in Cities
                    .HasForeignKey(d => d.CityName)
                    .HasConstraintName("FK_Users_Cities");
            });

            modelBuilder.Entity<BridgeClub>(entity =>
            {
                entity.HasKey(b => b.ClubID).HasName("PK__BridgeClubs__3214EC27");

                entity.Property(b => b.ClubName).HasMaxLength(100).IsRequired();

                entity.Property(b => b.CityName).HasMaxLength(30).IsRequired();

                // Configure one-to-many relationship between BridgeClub and City
                entity.HasOne(b => b.City)
                    .WithMany(c => c.Clubs)
                    .HasForeignKey(b => b.CityName)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Clubs_Cities");

                // Configure many-to-many relationship between Users and BridgeClubs
                entity.HasMany(b => b.Members)
                    .WithMany(u => u.Clubs)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserClub",
                        j => j.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.NoAction),
                        j => j.HasOne<BridgeClub>().WithMany().HasForeignKey("ClubId").OnDelete(DeleteBehavior.NoAction)
                    );
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
