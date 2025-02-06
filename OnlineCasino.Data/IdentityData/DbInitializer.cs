using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Areas.Identity.Data;

public static class DbInitializer
{
	public static async Task SeedRolesAndAdmin(IServiceProvider serviceProvider)
	{
		var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		var walletRepository = serviceProvider.GetRequiredService<IWalletRepository>();

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

				await CreateAdminWallet(walletRepository, adminUser.Id);
			}
		}
	}

	private static async Task CreateAdminWallet(IWalletRepository walletRepository, string userId)
	{
		int defaultCurrencyId = 1;

		walletRepository.CreateWallet(userId, defaultCurrencyId);
	}
}

