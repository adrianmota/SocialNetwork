using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Domain.Common;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Tables

            builder.Entity<User>()
                   .ToTable("Users");

            builder.Entity<Publication>()
                   .ToTable("Publications");

            builder.Entity<Comment>()
                   .ToTable("Comments");

            builder.Entity<Friend>()
                   .ToTable("Friends");

            builder.Entity<UserFriend>()
                   .ToTable("UserFriends");

            #endregion

            #region "Primary Keys"

            builder.Entity<User>()
                   .HasKey(user => user.Id);

            builder.Entity<Publication>()
                   .HasKey(publication => publication.Id);

            builder.Entity<Comment>()
                   .HasKey(comment => comment.Id);

            builder.Entity<Friend>()
                   .HasKey(Friend => Friend.Id);

            builder.Entity<UserFriend>()
                   .HasKey(uf => new { uf.UserId, uf.FriendId });

            #endregion

            #region Relationships

            builder.Entity<User>()
                   .HasMany(user => user.Publications)
                   .WithOne(publication => publication.User)
                   .HasForeignKey(publication => publication.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Friend>()
                   .HasMany(friend => friend.Publications)
                   .WithOne(publication => publication.Friend)
                   .HasForeignKey(publication => publication.FriendId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                   .HasMany(user => user.Comments)
                   .WithOne(comment => comment.User)
                   .HasForeignKey(comment => comment.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<UserFriend>()
                   .HasOne<User>(uf => uf.User)
                   .WithMany(user => user.UserFriends)
                   .HasForeignKey(uf => uf.UserId);

            builder.Entity<UserFriend>()
                   .HasOne<Friend>(uf => uf.Friend)
                   .WithMany(friend => friend.UserFriends)
                   .HasForeignKey(uf => uf.FriendId);

            builder.Entity<Publication>()
                   .HasMany(publication => publication.Comments)
                   .WithOne(comment => comment.Publication)
                   .HasForeignKey(comment => comment.PublicationId)
                   .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region "properties configuration"

            #region User
            builder.Entity<User>()
                   .Property("FirstName")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Entity<User>()
                   .Property("LastName")
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Entity<User>()
                   .Property("ProfilePhotoUrl")
                   .IsRequired(false);

            builder.Entity<User>()
                   .Property("Phone")
                   .IsRequired();

            builder.Entity<User>()
                   .Property("Email")
                   .IsRequired();

            builder.Entity<User>()
                   .Property("Username")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Entity<User>()
                   .Property("Password")
                   .IsRequired();
            #endregion

            #region Comments
            builder.Entity<Comment>()
                   .Property("Content")
                   .IsRequired();
            #endregion

            #region Friends
            builder.Entity<Friend>()
                   .Property("FirstName")
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Entity<Friend>()
                   .Property("LastName")
                   .HasMaxLength(25)
                   .IsRequired();

            builder.Entity<Friend>()
                   .Property("Username")
                   .HasMaxLength(50)
                   .IsRequired();
            #endregion

            #endregion
        }
    }
}
