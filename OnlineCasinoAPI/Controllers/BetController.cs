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
					return StatusCode(402, new { StatusCode = 402, Message = "Insufficient Balance!" });

				case -2:
					_logger.LogWarning("Bet failed: Incorrect currency.");
					return StatusCode(400, new { StatusCode = 400, Message = "Incorrect Currency!" });

				case -3:
					_logger.LogWarning("Bet failed: Invalid amount.");
					return StatusCode(407, new { StatusCode = 407, Message = "Invalid Amount!" });

				case -4:
					_logger.LogWarning("Bet failed: Invalid request.");
					return StatusCode(411, new { StatusCode = 411, Message = "Invalid Request!" });

				case -5:
					_logger.LogWarning("Bet failed: Inactive token.");
					return StatusCode(401, new { StatusCode = 401, Message = "Inactive Token!" });

				default:
					_logger.LogError("Unexpected error occurred during bet processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}
	}
}
