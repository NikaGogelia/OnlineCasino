using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Models.RepositoryModels;
using OnlineCasino.Repository.IRepository;

namespace OnlineCasino.Controllers;

public class CallbackController : Controller
{
	private readonly ILogger<CallbackController> _logger;
	private readonly ICallbackRepository _callbackRepository;

	public CallbackController(ILogger<CallbackController> logger, ICallbackRepository callbackRepository)
	{
		_logger = logger;
		_callbackRepository = callbackRepository;
	}

	[HttpPost("CompleteDeposit")]
	public async Task<IActionResult> CompleteDeposit([FromBody] CallbackRequest request)
	{
		_logger.LogInformation($"Received CompleteDeposit callback request: TransactionId: {request.TransactionId}, Amount: {request.Amount}, Status: {request.Status}");

		var callbackResponse = await _callbackRepository.CompleteDeposit(request.TransactionId, request.Amount, request.Status);

		_logger.LogInformation($"CompleteDeposit callback processed: TransactionId: {request.TransactionId}, Status: {callbackResponse.TransactionStatus}, Message: {callbackResponse.Message}");

		return Json(new { status = callbackResponse.TransactionStatus, message = callbackResponse.Message });
	}

	[HttpPost("CompleteWithdraw")]
	public async Task<IActionResult> CompleteWithdraw([FromBody] CallbackRequest request)
	{
		_logger.LogInformation($"Received CompleteWithdraw callback request: TransactionId: {request.TransactionId}, Amount: {request.Amount}, Status: {request.Status}");

		var callbackResponse = await _callbackRepository.CompleteWithdraw(request.TransactionId, request.Amount, request.Status);

		_logger.LogInformation($"CompleteWithdraw callback processed: TransactionId: {request.TransactionId}, Status: {callbackResponse.TransactionStatus}, Message: {callbackResponse.Message}");

		return Json(new { status = callbackResponse.TransactionStatus, message = callbackResponse.Message });
	}
}
