///////////////////////////////////////////////////////////////////
//
// Crionet TMS: Asset management system for sport events
// Copyright (c) Crionet
//
// Author: Youbiquitous Team
//


using Salotto.DomainModel.UserAccount;
using Microsoft.EntityFrameworkCore;
using Salotto.DomainModel.Activity;

namespace Salotto.Infrastructure.Persistence
{
    /// <summary>
    /// Database console for local tables of ZENZERO
    /// MODELING attributes
    /// </summary>
    public partial class SalottoDatabase 
    {
        /// <summary>
        /// Overridable model builder
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /////////////////////////////////////////////
            //  LOGICO ROLES
            //
            // Key
            modelBuilder.Entity<SalottoRole>()
                .Property(u => u.Role)
                .ValueGeneratedNever();
            modelBuilder.Entity<SalottoRole>()
                .HasKey(u => u.Role);
            // Owns One
            modelBuilder.Entity<SalottoRole>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  USERS
            //
            // Key
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
            // Required properties
            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Password)
                .IsRequired();
            // Foreign key(s)
            modelBuilder.Entity<User>()
                .HasOne(p => p.RoleInfo)
                .WithMany()
                .HasForeignKey(p => p.Role)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<User>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  EVENTS
            //
            // Key
            modelBuilder.Entity<Event>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Event>()
                .HasKey(u => u.Id);
            // Foreign key(s)
            modelBuilder.Entity<Event>()
                .HasOne(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<Event>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  EDITIONS
            //
            // Key
            modelBuilder.Entity<Edition>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Edition>()
                .HasKey(u => u.Id);
            // Foreign key(s)
            modelBuilder.Entity<Edition>()
                .HasOne(u => u.Event)
                .WithMany()
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Edition>()
                .HasOne(u => u.ApprovedByUser)
                .WithMany()
                .HasForeignKey(p => p.ApprovedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<Edition>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  CARDS
            //
            // Key
            modelBuilder.Entity<Card>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Card>()
                .HasKey(u => u.Id);
            // Foreign key(s)
            modelBuilder.Entity<Card>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Card>()
                .HasOne(u => u.ApprovedByUser)
                .WithMany()
                .HasForeignKey(p => p.ApprovedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<Card>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  POSTS
            //
            // Key
            modelBuilder.Entity<Post>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Post>()
                .HasKey(u => u.Id);
            // Foreign key(s)
            modelBuilder.Entity<Post>()
                .HasOne(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Post>()
                .HasOne(u => u.ApprovedByUser)
                .WithMany()
                .HasForeignKey(p => p.ApprovedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Post>()
                .HasOne(u => u.Edition)
                .WithMany()
                .IsRequired(false)
                .HasForeignKey(p => p.EditionId)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<Post>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  ACCESS LOGS
            //
            // Key
            modelBuilder.Entity<AccessLog>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<AccessLog>()
                .HasKey(u => u.Id);
            // Foreign key(s)
            modelBuilder.Entity<AccessLog>()
                .HasOne(u => u.ScannedByUser)
                .WithMany()
                .HasForeignKey(p => p.ScannedByUserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AccessLog>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<AccessLog>()
                .HasOne(u => u.Edition)
                .WithMany()
                .HasForeignKey(p => p.EditionId)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<AccessLog>().OwnsOne(u => u.TimeStamp);

            /////////////////////////////////////////////
            //  USER-EDITION BINDINGS
            //
            // Key
            modelBuilder.Entity<UserEditionBinding>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<UserEditionBinding>()
                .HasKey(u => u.Id);
            // Foreign key(s)
            modelBuilder.Entity<UserEditionBinding>()
                .HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserEditionBinding>()
                .HasOne(u => u.Edition)
                .WithMany()
                .HasForeignKey(p => p.EditionId)
                .OnDelete(DeleteBehavior.NoAction);
            // Owns One
            modelBuilder.Entity<UserEditionBinding>().OwnsOne(u => u.TimeStamp);

        }
    }
}
