using Microsoft.AspNetCore.Mvc;

namespace OnlineCasino.Controllers;

public class DepositController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
