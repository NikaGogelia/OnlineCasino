using Microsoft.AspNetCore.Mvc;
using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BetController : ControllerBase
	{
		private readonly IBetService _betService;
		private readonly ILogger<BetController> _logger;

		public BetController(IBetService betService, ILogger<BetController> logger)
		{
			_betService = betService;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterBet([FromBody] BetRequest request)
		{
			var response = await _betService.Bet(request);

			_logger.LogInformation("Bet service response: {@Response}", response);

			switch (response.Status)
			{
				case 1:
					_logger.LogInformation("Bet registered successfully. TransactionId: {TransactionId}, CurrentBalance: {CurrentBalance}",
						response.TransactionId, response.CurrentBalance);
					return Ok(new
					{
						StatusCode = 200,
						Data = new
						{
							TransactionId = response.TransactionId?.ToString(),
							CurrentBalance = response.CurrentBalance
						}
					});

				case -1:
					_logger.LogWarning("Bet failed: Insufficient balance.");
					return StatusCode(400, new { StatusCode = 402 });

				case -2:
					_logger.LogWarning("Bet failed: Incorrect currency.");
					return StatusCode(400, new { StatusCode = 400 });

				case -3:
					_logger.LogWarning("Bet failed: Invalid amount.");
					return StatusCode(400, new { StatusCode = 407 });

				case -4:
					_logger.LogWarning("Bet failed: Invalid request.");
					return StatusCode(400, new { StatusCode = 411 });

				case -5:
					_logger.LogWarning("Bet failed: Inactive token.");
					return StatusCode(400, new { StatusCode = 401 });

				default:
					_logger.LogError("Unexpected error occurred during bet processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}

		[HttpPost("Cancel")]
		public async Task<IActionResult> CancelBet([FromBody] CancelBetRequest request)
		{
			var response = await _betService.Cancel(request);

			_logger.LogInformation("Cancel Bet service response: {@Response}", response);

			switch (response.Status)
			{
				case 1:
					_logger.LogInformation("Bet canceled successfully. TransactionId: {TransactionId}, CurrentBalance: {CurrentBalance}",
						response.TransactionId, response.CurrentBalance);
					return Ok(new
					{
						StatusCode = 200,
						Data = new
						{
							TransactionId = response.TransactionId?.ToString(),
							CurrentBalance = response.CurrentBalance
						}
					});

				case -2:
					_logger.LogWarning("Bet cancellation failed: Incorrect currency.");
					return StatusCode(400, new { StatusCode = 400 });

				case -3:
					_logger.LogWarning("Bet cancellation failed: Invalid amount.");
					return StatusCode(400, new { StatusCode = 407 });

				case -4:
					_logger.LogWarning("Bet cancellation failed: Invalid request.");
					return StatusCode(400, new { StatusCode = 411 });

				case -5:
					_logger.LogWarning("Bet cancellation failed: Inactive token.");
					return StatusCode(400, new { StatusCode = 401 });

				default:
					_logger.LogError("Unexpected error occurred during bet cancellation processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}
	}
}
