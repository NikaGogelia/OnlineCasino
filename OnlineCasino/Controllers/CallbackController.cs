using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers;

public class CallbackController : Controller
{
	private readonly ICallbackRepository _callbackRepository;

	public CallbackController(ICallbackRepository callbackRepository)
	{
		_callbackRepository = callbackRepository;
	}

	[HttpPost("CompleteDeposit")]
	public async Task<IActionResult> CompleteDeposit([FromBody] CallbackRequest request)
	{
		var callbackResponse = await _callbackRepository.CompleteDeposit(request.TransactionId, request.Amount, request.Status);

		return Json(new { status = callbackResponse.TransactionStatus, message = callbackResponse.Message });
	}
}
