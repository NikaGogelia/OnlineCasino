using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineCasino.Controllers
{
	[Authorize(Roles = "Admin, Player")]
	public class TransactionsHistoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
