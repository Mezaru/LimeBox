﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LimeBox.Models.Entities
{
    public partial class LimeContext : DbContext
    {
        public virtual DbSet<Boxes> Boxes { get; set; }
        public virtual DbSet<OrderRows> OrderRows { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(Startup.connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boxes>(entity =>
            {
                entity.ToTable("Boxes", "Lime");

                entity.HasIndex(e => new { e.BoxId, e.BoxType })
                    .HasName("UQ__Boxes__2C4BB083FE8FBB48")
                    .IsUnique();

                entity.Property(e => e.BoxId).HasColumnName("Box_Id");

                entity.Property(e => e.BoxType)
                    .IsRequired()
                    .HasColumnName("Box_Type")
                    .HasMaxLength(50);

                entity.Property(e => e.BoxValue).HasColumnName("Box_Value");
            });

            modelBuilder.Entity<OrderRows>(entity =>
            {
                entity.ToTable("Order_Rows", "Lime");

                entity.Property(e => e.BoxId).HasColumnName("Box_Id");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderRows)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Rows_ToTable");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "Lime");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "Lime");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.AspNetId)
                    .IsRequired()
                    .HasColumnName("AspNet_Id")
                    .HasMaxLength(450);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}