using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlazorBackOfficeCamilla.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    public DbSet<UserAddress> UserAddresses { get; set; }

    public DbSet<UserProfile> UserProfiles { get; set; }
}
