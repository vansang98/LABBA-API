using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ManagerEmployee.EfCore.Models
{
    /// <summary>Context kết nối cơ sở dữ liệu</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public partial class BALabTestContext : DbContext
    {
        public BALabTestContext()
        {
        }

        public BALabTestContext(DbContextOptions<BALabTestContext> options)
            : base(options)
        {
        }

        /// <summary>Gets or sets the employees.</summary>
        /// <value>The employees.</value>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PM-SANGNV;Database=BALabTest;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Birthday).HasColumnType("datetime");

                entity.Property(e => e.Creattime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Fullname).HasMaxLength(50);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phonenumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Updatetime).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(35);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
