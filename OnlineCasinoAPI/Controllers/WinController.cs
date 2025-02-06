using Microsoft.AspNetCore.Mvc;
using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WinController : ControllerBase
	{
		private readonly IWinService _winService;
		private readonly ILogger<WinController> _logger;

		public WinController(IWinService winService, ILogger<WinController> logger)
		{
			_winService = winService;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterWin([FromBody] WinRequest request)
		{
			var response = await _winService.Win(request);

			_logger.LogInformation("Win service response: {@Response}", response);

			switch (response.Status)
			{
				case 1:
					_logger.LogInformation("Win registered successfully. TransactionId: {TransactionId}, CurrentBalance: {CurrentBalance}",
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
					_logger.LogWarning("Win failed: Incorrect currency.");
					return StatusCode(400, new { StatusCode = 400 });

				case -3:
					_logger.LogWarning("Win failed: Invalid amount.");
					return StatusCode(400, new { StatusCode = 407 });

				case -4:
					_logger.LogWarning("Win failed: Invalid request.");
					return StatusCode(400, new { StatusCode = 411 });

				case -5:
					_logger.LogWarning("Win failed: Inactive token.");
					return StatusCode(400, new { StatusCode = 401 });

				default:
					_logger.LogError("Unexpected error occurred during Win processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}

		[HttpPost("Change")]
		public async Task<IActionResult> ChangeWin([FromBody] ChangeWinRequest request)
		{
			var response = await _winService.Change(request);

			_logger.LogInformation("Change Win service response: {@Response}", response);

			switch (response.Status)
			{
				case 1:
					_logger.LogInformation("Change Win registered successfully. TransactionId: {TransactionId}, CurrentBalance: {CurrentBalance}",
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
					_logger.LogWarning("Change Win failed: Incorrect currency.");
					return StatusCode(400, new { StatusCode = 400 });

				case -3:
					_logger.LogWarning("Change Win failed: Invalid amount.");
					return StatusCode(400, new { StatusCode = 407 });

				case -4:
					_logger.LogWarning("Change Win failed: Invalid request.");
					return StatusCode(400, new { StatusCode = 411 });

				case -5:
					_logger.LogWarning("Change Win failed: Inactive token.");
					return StatusCode(400, new { StatusCode = 401 });

				default:
					_logger.LogError("Unexpected error occurred during Change Win processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}
	}
}
