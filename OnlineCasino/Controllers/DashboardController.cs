using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers;

[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
	private IDepositWithdrawRepository _depositWithdrawRepository;

	public DashboardController(IDepositWithdrawRepository depositWithdrawRepository)
	{
		_depositWithdrawRepository = depositWithdrawRepository;
	}

	public IActionResult Index()
	{
		return View();
	}

	[HttpGet("TransactionRequests")]
	public async Task<IActionResult> GetTransactionRequests()
	{
		var transactionRequests = await _depositWithdrawRepository.GetRegisteredTransactionRequests();

		return Ok(transactionRequests);
	}
}
