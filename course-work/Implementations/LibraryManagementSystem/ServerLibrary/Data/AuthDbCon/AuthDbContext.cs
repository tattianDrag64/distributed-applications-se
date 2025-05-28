using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BaseLibrary.Entities;
namespace ServerLibrary.Data.AuthDbCon
{

     public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<MyApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            string adminId = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string adminRoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210";
            string adminPassword = "Admin123";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN".ToUpper() },
                new IdentityRole { Id = "5c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Librarian", NormalizedName = "LIBRARIAN".ToUpper() },
                new IdentityRole { Id = "6c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Member", NormalizedName = "MEMBER".ToUpper() });

            //create user
            var appUser = new MyApplicationUser
            {
                Id = adminId,
                Email = "admin@admin.com",
                EmailConfirmed = true,
                UserName = "admin@admin.com",

            };

            // set user password
            PasswordHasher<MyApplicationUser> ph = new PasswordHasher<MyApplicationUser>();
            appUser.PasswordHash = ph.HashPassword(appUser, adminPassword);

            //set user role to admin
            builder.Entity<MyApplicationUser>().HasData(appUser);

            //set user role to admin
            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string> { RoleId = adminRoleId, UserId = adminId });

            base.OnModelCreating(builder);

        }
    }
}
