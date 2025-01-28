using Microsoft.AspNetCore.Mvc;
using OnlineCasinoAPI.Models;
using OnlineCasinoAPI.Services.IService;

namespace OnlineCasinoAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
	private readonly ILogger<TokenController> _logger;
	private readonly ITokenService _tokenService;

	public TokenController(
		ILogger<TokenController> logger,
		ITokenService tokenService
	)
	{
		_logger = logger;
		_tokenService = tokenService;
	}

	[HttpPost("Auth")]
	public async Task<IActionResult> Auth([FromBody] TokenRequest request)
	{
		var result = await _tokenService.AuthToken(request.PublicToken);

		if (result.Status == 0)
		{
			_logger.LogError("Something Went Wrong With Auth!");
			return StatusCode(500, new { StatusCode = 500 });
		}

		_logger.LogInformation("Token Authenticated Successfully!");
		return Ok(new
		{
			StatusCode = 200,
			Data = new
			{
				PrivateToken = result.PrivateToken
			}
		});
	}
}
