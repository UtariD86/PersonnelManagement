using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Concrete.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);

            builder.Property(u => u.Name).IsRequired();
            builder.Property(u => u.Name).HasMaxLength(250);

            builder.Property(u => u.Surname).IsRequired();
            builder.Property(u => u.Surname).HasMaxLength(250);
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("AspNetUsers");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(50);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(100);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(100);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<UserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<UserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var adminUser = new User
            {
                Id = 1,
                UserName = "adminuser",
                NormalizedUserName = "ADMINUSER",
                Email = "adminuser@gmail.com",
                NormalizedEmail = "ADMINUSER@GMAIL.COM",
                PhoneNumber = "+905555555555",
                Name = "big",
                Surname = "boss",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Picture = "default.jpg",
            };
            adminUser.PasswordHash = CreatePasswordHash(adminUser, "adminuser");

            var userUser = new User
            {
                Id = 2,
                UserName = "littleslave",
                NormalizedUserName = "LITTLESLAVE",
                Email = "littleslave@zurafworks.com",
                NormalizedEmail = "LITTLESLAVE@ZURAFWORKS.COM",
                PhoneNumber = "+905555555555",
                Name = "little",
                Surname = "slave",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                Picture = "default.jpg",
            };
            userUser.PasswordHash = CreatePasswordHash(userUser, "personeltakip");
            builder.HasData(adminUser, userUser);
        }
        private string CreatePasswordHash(User user, string password)
        {
            var passwordHasher = new PasswordHasher<User>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}
