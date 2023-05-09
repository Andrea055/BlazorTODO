using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Controllers;
using todo_blazor_auth.Shared.Models;

namespace todo_blazor_auth.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public DbSet<Work> Works { get; set; }
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Work>().Property(b => b.UserId).HasColumnName("UserId"); // Relation with user
        base.OnModelCreating(builder);
        
    }
}
