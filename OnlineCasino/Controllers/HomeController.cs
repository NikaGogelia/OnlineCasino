using Microsoft.AspNetCore.Mvc;

namespace OnlineCasino.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
