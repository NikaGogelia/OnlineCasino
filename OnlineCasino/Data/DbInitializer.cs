using Microsoft.AspNetCore.Identity;
using OnlineCasino.Areas.Identity.Data;

namespace OnlineCasino.Data;

public static class DbInitializer
{
	public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
	{
		var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

		string[] roles = { "Admin", "Player" };
		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}

		var adminEmail = "admin@example.com";
		var adminPassword = "Admin@123";
		if (await userManager.FindByEmailAsync(adminEmail) == null)
		{
			var adminUser = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
			var result = await userManager.CreateAsync(adminUser, adminPassword);
			if (result.Succeeded)
			{
				await userManager.AddToRoleAsync(adminUser, "Admin");
			}
		}
	}
}
