using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineCasino.Areas.Identity.Data;

public class IdentityDbContext : IdentityDbContext<ApplicationUser>
{
	public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
	}
}
