using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WithdrawController : ControllerBase
	{
		private readonly ILogger<WithdrawController> _logger;
		private readonly IWithdrawService _withdrawService;

		public WithdrawController(ILogger<WithdrawController> logger, IWithdrawService withdrawService)
		{
			_logger = logger;
			_withdrawService = withdrawService;
		}

		[HttpPost]
		public async Task<IActionResult> ProcessWithdraw([FromBody] WithdrawRequest request)
		{
			_logger.LogInformation($"Received WithdrawRequest: {@request}");

			var result = await _withdrawService.ProcessWithdrawRequest(request);

			_logger.LogInformation($"WithdrawRequest processed. Result: {@result}");

			var callbackRequest = new CallbackRequest { Amount = request.Amount, TransactionId = request.TransactionId, Status = result.Status };

			_logger.LogInformation($"Sending CallbackRequest: {@callbackRequest}");

			var callbackResponse = await _withdrawService.SendRequestToCallback(callbackRequest);

			_logger.LogInformation($"CallbackRequest processed. Response: {@callbackResponse}");

			return Ok(result);
		}
	}
}
