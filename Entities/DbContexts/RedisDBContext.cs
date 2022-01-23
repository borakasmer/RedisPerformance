using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RedisExample.Entities;

namespace RedisExample.Entities.DbContexts
{
    public partial class RedisDBContext : DbContext
    {
        public RedisDBContext()
        {
        }

        public RedisDBContext(DbContextOptions<RedisDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DbSecurityAction> DbSecurityActions { get; set; }
        public virtual DbSet<DbSecurityController> DbSecurityControllers { get; set; }
        public virtual DbSet<DbSecurityUserAction> DbSecurityUserActions { get; set; }
        public virtual DbSet<DbUser> DbUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=RedisDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbSecurityAction>(entity =>
            {
                entity.HasKey(e => e.IdSecurityAction);

                entity.ToTable("DB_SECURITY_ACTION");

                entity.Property(e => e.ActionName).HasMaxLength(100);

                entity.Property(e => e.CreDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdSecurityControllerNavigation)
                    .WithMany(p => p.DbSecurityActions)
                    .HasForeignKey(d => d.IdSecurityController)
                    .HasConstraintName("FK_DB_SECURITY_ACTION_DB_SECURITY_CONTROLLER");
            });

            modelBuilder.Entity<DbSecurityController>(entity =>
            {
                entity.HasKey(e => e.IdSecurityController);

                entity.ToTable("DB_SECURITY_CONTROLLER");

                entity.Property(e => e.ControllerName).HasMaxLength(100);

                entity.Property(e => e.CreDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isDeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<DbSecurityUserAction>(entity =>
            {
                entity.HasKey(e => e.IdSecurityUserAction);

                entity.ToTable("DB_SECURITY_USER_ACTION");

                entity.Property(e => e.CreDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdSecurityControllerNavigation)
                    .WithMany(p => p.DbSecurityUserActions)
                    .HasForeignKey(d => d.IdSecurityController)
                    .HasConstraintName("FK_DB_SECURITY_USER_ACTION_DB_SECURITY_CONTROLLER");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.DbSecurityUserActions)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_DB_SECURITY_USER_ACTION_DB_USER");
            });

            modelBuilder.Entity<DbUser>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("DB_USER");

                entity.Property(e => e.CreDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gsm)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IsAdmin).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
