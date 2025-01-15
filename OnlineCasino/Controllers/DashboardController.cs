using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers;

[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
	private readonly UserManager<ApplicationUser> _user;
	private IDepositWithdrawRepository _depositWithdrawRepository;

	public DashboardController(UserManager<ApplicationUser> user, IDepositWithdrawRepository depositWithdrawRepository)
	{
		_user = user;
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

	[HttpPut("RejectWithdrawRequest")]
	public async Task<IActionResult> RejectWithdrawRequest([FromBody] int id)
	{
		//var user = await _user.FindByNameAsync(userName);
		//if (user == null)
		//{
		//	return Unauthorized();
		//}

		var reject = await _depositWithdrawRepository.RejectWithdrawRequest(id);

		return Ok(reject);
	}
}
