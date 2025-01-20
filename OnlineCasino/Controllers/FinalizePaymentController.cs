using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers;

[Authorize(Roles = "Admin, Player")]
public class FinalizePaymentController : Controller
{
	private readonly IDepositWithdrawRepository _depositWithdrawRepository;

	public FinalizePaymentController(IDepositWithdrawRepository depositWithdrawRepository)
	{
		_depositWithdrawRepository = depositWithdrawRepository;
	}

	[HttpGet("FinalizePayment/Deposit/{transactionId}/{status}")]
	public async Task<IActionResult> Deposit(int transactionId, string status)
	{
		var transaction = await _depositWithdrawRepository.GetRegisteredTransactionRequest(transactionId);

		if (transaction == null)
		{
			return NotFound("Transaction not found.");
		}

		if (status.ToLower() == "success")
		{
			ViewBag.StatusMessage = "Deposit successful!";
			ViewBag.Status = "Success";
		}
		else if (status.ToLower() == "rejected")
		{
			ViewBag.StatusMessage = "Deposit was rejected.";
			ViewBag.Status = "Rejected";
		}
		else
		{
			return BadRequest("Invalid status.");
		}

		return View(transaction);
	}
}
