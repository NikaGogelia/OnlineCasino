using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepositController : ControllerBase
	{
		private readonly ILogger<DepositController> _logger;
		private readonly IDepositService _depositService;

		public DepositController(ILogger<DepositController> logger, IDepositService depositService)
		{
			_logger = logger;
			_depositService = depositService;
		}

		[HttpPost]
		public async Task<IActionResult> ProcessDeposit([FromBody] DepositRequest request)
		{
			_logger.LogInformation($"Received DepositRequest: {@request}");

			var result = await _depositService.ProcessDepositRequest(request);

			_logger.LogInformation($"DepositRequest processed. Result: {@result}");

			return Ok(result);
		}

		[HttpPost("CompleteDepositSendToCallback")]
		public async Task<IActionResult> CompleteDepositSendToCallback([FromBody] CallbackRequest request)
		{
			_logger.LogInformation($"Received CompleteDepositSendToCallback request: {@request}");

			var response = await _depositService.SendRequestToCallback(request);

			_logger.LogInformation($"CompleteDepositSendToCallback processed. Response: {@response}");

			return Ok(response);
		}
	}
}
