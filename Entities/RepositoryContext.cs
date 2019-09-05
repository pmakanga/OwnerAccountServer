using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext: DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("OwnerId");

                entity.Property(e => e.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasColumnName("Address")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                     .HasColumnName("DateOfBirth")
                     .HasColumnType("date");


            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("AccountId");
                entity.Property(e => e.OwnerId).HasColumnName("OwnerId");

                entity.Property(e => e.AccountType)
                    .HasColumnName("AccountType")
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated)
                    .HasColumnName("DateCreated")
                    .HasColumnType("date");

                entity.HasOne(o => o.Owner)
                     .WithMany(a => a.Accounts)
                     .HasForeignKey(o => o.OwnerId)
                     .HasConstraintName("fk_Account_Owner");
            });
        }
    }
}
