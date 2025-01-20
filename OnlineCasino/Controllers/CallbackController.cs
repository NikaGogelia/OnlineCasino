using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Models.RepositoryModels;
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

	[HttpPost("CompleteWithdraw")]
	public async Task<IActionResult> CompleteWithdraw([FromBody] CallbackRequest request)
	{
		var callbackResponse = await _callbackRepository.CompleteWithdraw(request.TransactionId, request.Amount, request.Status);

		return Json(new { status = callbackResponse.TransactionStatus, message = callbackResponse.Message });
	}
}
