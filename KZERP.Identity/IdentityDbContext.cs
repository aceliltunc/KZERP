using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using KZERP.Identity.AppUser;


namespace KZERP.Identity.IdentityDbContext
{
    public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfig());
            // builder.Entity<IdentityRole>().HasData(
            //     new IdentityRole
            //     {
            //         Id = "11111111-1111-1111-1111-111111111111",
            //         Name = "Admin",
            //         NormalizedName = "ADMIN",
            //         ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
            //     },
            //     new IdentityRole
            //     {
            //         Id = "22222222-2222-2222-2222-222222222222",
            //         Name = "Engineer",
            //         NormalizedName = "ENGINEER",
            //         ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
            //     },
            //     new IdentityRole
            //     {
            //         Id = "33333333-3333-3333-3333-333333333333",
            //         Name = "Worker",
            //         NormalizedName = "WORKER",
            //         ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
            //     },
            //     new IdentityRole
            //     {
            //         Id = "44444444-4444-4444-4444-444444444444",
            //         Name = "User",
            //         NormalizedName = "USER",
            //         ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
            //     }
            // );

            // var adminId = "87c4175d-0d9e-45b2-9baa-1a67f9bf1c46";

            // var hasher = new PasswordHasher<ApplicationUser>();

            // var adminUser = new ApplicationUser
            // {
            //     Id = adminId,
            //     FullName = "admin",
            //     UserName = "admin",
            //     NormalizedUserName = "ADMIN",
            //     Email = "admin@kzerp.com",
            //     NormalizedEmail = "ADMIN@KZERP.COM",
            //     JobTitle = "Admin",
            //     Department = "Admin",
            //     SecurityStamp = "ZXAUFKCNWRGFPPFA44ZGL6A3L243SL6S"
            // };

            // adminUser.PasswordHash = hasher.HashPassword(adminUser, "kznimda$74");

            // builder.Entity< ApplicationUser > ().HasData(adminUser);

            // builder.Entity<IdentityUserRole<string>>().HasData(
            //     new IdentityUserRole<string>
            //     {
            //         UserId = adminId,
            //         RoleId = "11111111-1111-1111-1111-111111111111"
            //     }
            // );






        }
    }
}