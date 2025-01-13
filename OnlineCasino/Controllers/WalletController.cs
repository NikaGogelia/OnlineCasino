using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers
{
	[Authorize(Roles = "Player")]
	public class WalletController : Controller
	{
		private readonly UserManager<ApplicationUser> _user;
		private readonly IWalletRepository _walletRepository;

		public WalletController(UserManager<ApplicationUser> userManager, IWalletRepository walletRepository)
		{
			_user = userManager;
			_walletRepository = walletRepository;
		}

		[HttpGet("balance")]
		public async Task<IActionResult> GetWalletBallanceAsync()
		{
			var user = await _user.GetUserAsync(User);
			if (user == null)
			{
				return Unauthorized();
			}

			var isPlayer = await _user.IsInRoleAsync(user, "Player");
			if (!isPlayer)
			{
				return Forbid("Access denied. Only players can access wallet balance.");
			}

			var wallet = await _walletRepository.GetWalletBalance(user.Id);
			if (wallet == null)
			{
				return NotFound();
			}

			return Ok(wallet);
		}
	}
}
