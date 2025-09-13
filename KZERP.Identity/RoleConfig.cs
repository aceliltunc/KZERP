using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KZERP.Identity
{
    public class RoleConfig : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole 
                { 
                    Id = "11111111-1111-1111-1111-111111111111", 
                    Name = "Admin", 
                    NormalizedName = "ADMIN" 
                },
                new IdentityRole 
                { 
                    Id = "22222222-2222-2222-2222-222222222222", 
                    Name = "Engineer", 
                    NormalizedName = "ENGINEER" 
                },
                new IdentityRole 
                { 
                    Id = "33333333-3333-3333-3333-333333333333", 
                    Name = "Worker", 
                    NormalizedName = "WORKER" 
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
