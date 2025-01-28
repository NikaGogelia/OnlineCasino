using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Repository.IRepository;
using OnlineCasino.Service.IService;

namespace OnlineCasino.Controllers;

[Authorize(Roles = "Admin, Player")]
public class TokenController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly ILogger<DepositWithdrawController> _logger;
	private readonly ITokenRepository _tokenRepository;

	public TokenController(
		UserManager<ApplicationUser> userManager,
		ILogger<DepositWithdrawController> logger,
		ITokenRepository tokenRepository,
		IBankingService bankingService
	)
	{
		_userManager = userManager;
		_logger = logger;
		_tokenRepository = tokenRepository;
	}

	[HttpPost("CreatePublicToken")]
	public async Task<IActionResult> CreatePublicToken()
	{
		var userId = _userManager.GetUserId(User);

		if (userId == null)
		{
			_logger.LogError("User Not Found!");
			return Json(new { status = 0, message = "User Not Found!" });
		}

		Guid token = Guid.NewGuid();

		var createToken = await _tokenRepository.CreatePublicToken(userId, token);

		if (createToken.Status == 0)
		{
			_logger.LogError(createToken.Message);
			return Json(new { status = createToken.Status, message = createToken.Message });
		}

		_logger.LogInformation(createToken.Message);
		return Json(new { status = createToken.Status, message = createToken.Message, publicToken = createToken.PublicToken });
	}
}
