using Microsoft.AspNetCore.Mvc;
using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayerController : ControllerBase
	{
		private readonly IPlayerService _playerService;
		private readonly ILogger<PlayerController> _logger;

		public PlayerController(IPlayerService playerService, ILogger<PlayerController> logger)
		{
			_playerService = playerService;
			_logger = logger;
		}

		[HttpPost("Balance")]
		public async Task<IActionResult> GetBalance([FromBody] GetBalanceRequest request)
		{
			var response = await _playerService.GetPlayerBalance(request);

			_logger.LogInformation("GetBalance service response: {@Response}", response);

			switch (response.Status)
			{
				case 1:
					_logger.LogInformation("GetBalance is Successful. CurrentBalance: {CurrentBalance}", response.CurrentBalance);
					return Ok(new
					{
						StatusCode = 200,
						Data = new
						{
							CurrentBalance = response.CurrentBalance
						}
					});

				case -2:
					_logger.LogWarning("GetBalance failed: Incorrect currency.");
					return StatusCode(400, new { StatusCode = 400 });

				case -5:
					_logger.LogWarning("GetBalance failed: Inactive token.");
					return StatusCode(401, new { StatusCode = 401 });

				default:
					_logger.LogError("Unexpected error occurred during bet processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}

		[HttpPost("PlayerInfo")]
		public async Task<IActionResult> GetPlayerInfo([FromBody] GetPlayerInfoRequest request)
		{
			var response = await _playerService.GetPlayerInfo(request);

			_logger.LogInformation("GetPlayerInfo service response: {@Response}", response);

			switch (response.Status)
			{
				case 1:
					_logger.LogInformation("GetPlayerInfo is Successful. CurrentBalance: {CurrentBalance}", response.CurrentBalance);
					return Ok(new
					{
						StatusCode = 200,
						Data = new
						{
							UserId = response.UserId,
							UserName = response.UserName,
							Email = response.Email,
							Currency = response.Currency,
							CurrentBalance = response.CurrentBalance
						}
					});

				case -5:
					_logger.LogWarning("GetPlayerInfo failed: Inactive token.");
					return StatusCode(401, new { StatusCode = 401 });

				default:
					_logger.LogError("GetPlayerInfo error occurred during bet processing.");
					return StatusCode(500, new { StatusCode = 500 });
			}
		}
	}
}
