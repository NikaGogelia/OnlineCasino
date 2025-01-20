using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers
{
	[Authorize(Roles = "Admin, Player")]
	public class TransactionsHistoryController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly ITransactionsRepository _transactionsRepository;

		public TransactionsHistoryController(UserManager<ApplicationUser> userManager, ITransactionsRepository transactionsRepository)
		{
			_userManager = userManager;
			_transactionsRepository = transactionsRepository;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("TransactionsForCurrentUser")]
		public async Task<IActionResult> GetAllTransactions()
		{
			var userId = _userManager.GetUserId(User);

			var transactions = await _transactionsRepository.GetAllTransactionsForCurrentUser(userId);

			return Ok(transactions);
		}
	}
}
