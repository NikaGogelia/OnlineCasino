using Microsoft.AspNetCore.Mvc;

namespace OnlineCasino.Controllers;

public class DashboardController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
