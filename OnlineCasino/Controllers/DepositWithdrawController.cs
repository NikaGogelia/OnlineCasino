using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Models;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers;

[Authorize(Roles = "Admin, Player")]
public class DepositWithdrawController : Controller
{
	private IDepositWithdrawRepository _depositWithdrawRepository;
	private readonly UserManager<ApplicationUser> _userManager;

	public DepositWithdrawController(IDepositWithdrawRepository depositWithdrawRepository, UserManager<ApplicationUser> userManager)
	{
		_depositWithdrawRepository = depositWithdrawRepository;
		_userManager = userManager;
	}

	public IActionResult Deposit()
	{
		return View();
	}

	public IActionResult Withdraw()
	{
		return View();
	}

	[HttpPost("RegisterTransaction")]
	public async Task<IActionResult> RegisterTransactionAsync([FromBody] TransactionRequest request)
	{
		var user = await _userManager.GetUserAsync(User);

		if (user == null)
		{
			return Unauthorized("User is not authenticated.");
		}

		if (request.TransactionType != "deposit" && request.TransactionType != "withdraw")
		{
			return BadRequest("Transaction Type Should Be: 'deposit' or 'withdraw'!");
		}

		if (request.Amount <= 0)
		{
			return BadRequest("Transaction Amount Should Be Greater Than 0!");
		}

		try
		{
			var transaction = await _depositWithdrawRepository.TransactionRequest(user.Id, request.Amount, request.TransactionType);

			return Ok(transaction);
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Internal server error: {ex.Message}");
		}
	}
}
