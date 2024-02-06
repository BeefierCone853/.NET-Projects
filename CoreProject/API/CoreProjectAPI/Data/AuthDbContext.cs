using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreProjectAPI.Data;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        const string readerRoleId = "28d65a5b-a7db-4850-b480-835917d7531";
        const string writerRoleId = "13f27b9b-e1gj-7219-f581-845915d7531";
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = readerRoleId,
                Name = "Reader",
                NormalizedName = "Reader".ToUpper(),
                ConcurrencyStamp = readerRoleId
            },
            new IdentityRole
            {
                Id = writerRoleId,
                Name = "Writer",
                NormalizedName = "Writer".ToUpper(),
                ConcurrencyStamp = writerRoleId
            }
        };
        builder.Entity<IdentityRole>().HasData(roles);
        const string adminUserId = "26u16q3n-e2gn-1259-s152-147132n8633";
        var admin = new IdentityUser()
        {
            Id = adminUserId,
            UserName = "admin@coreproject.com",
            Email = "admin@coreproject.com",
            NormalizedEmail = "admin@coreproject.com".ToUpper(),
            NormalizedUserName = "admin@coreproject.com".ToUpper()
        };
        admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
        builder.Entity<IdentityUser>().HasData(admin);
        var adminRoles = new List<IdentityUserRole<string>>()
        {
            new()
            {
                UserId = adminUserId,
                RoleId = readerRoleId
            },
            new()
            {
                UserId = adminUserId,
                RoleId = writerRoleId
            },
        };
        builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
}