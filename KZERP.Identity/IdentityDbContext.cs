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

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
                },
                new IdentityRole
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "Engineer",
                    NormalizedName = "ENGINEER",
                    ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
                },
                new IdentityRole
                {
                    Id = "33333333-3333-3333-3333-333333333333",
                    Name = "Worker",
                    NormalizedName = "WORKER",
                    ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
                },
                new IdentityRole
                {
                    Id = "44444444-4444-4444-4444-444444444444",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "EBF296D8-18C8-42A9-93C1-4D30B9557C4F"
                }
            );
        }
    }
}