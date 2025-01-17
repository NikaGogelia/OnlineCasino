using BankApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OnlineCasino.Areas.Identity.Data;
using OnlineCasino.Models;
using OnlineCasino.Models.Banking;
using OnlineCasino.Repository.IRepository;
using OnlineCasino.Service;
using OnlineCasino.Service.IService;

namespace OnlineCasino.Controllers;

[Authorize(Roles = "Admin, Player")]
public class DepositWithdrawController : Controller
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly IDepositWithdrawRepository _depositWithdrawRepository;
	private readonly IBankingService _bankingService;
	private readonly AppSettings _appSettings;

	public DepositWithdrawController(IDepositWithdrawRepository depositWithdrawRepository, UserManager<ApplicationUser> userManager, IBankingService bankingService, IOptions<AppSettings> appSettings)
	{
		_userManager = userManager;
		_depositWithdrawRepository = depositWithdrawRepository;
		_bankingService = bankingService;
		_appSettings = appSettings.Value;
	}

	public IActionResult Deposit()
	{
		return View();
	}

	public IActionResult Withdraw()
	{
		return View();
	}

	[HttpPost("RegisterDeposit")]
	public async Task<IActionResult> RegisterDeposit([FromBody] TransactionRequest request)
	{
		var userId = _userManager.GetUserId(User);

		if (userId == null)
		{
			return Json(new { status = 0, message = "user not found" });
		}

		var result = await _depositWithdrawRepository.RegisterDeposit(userId, request.Amount);

		if (result.Status != 1)
		{
			return Json(new { status = result.Status, message = "Something went wrong" });
		}

		var apiRequest = new BankingDepositRequest
		{
			Amount = request.Amount,
			TransactionId = result.TransactionId,
			MerchantId = _appSettings.MerchantId,
			Hash = HashDepositService.GenerateHash(request.Amount, _appSettings.MerchantId, result.TransactionId, _appSettings.SecretKey)
		};

		var bankingApiResponse = await _bankingService.SendDepositRequestAsync(apiRequest);

		return Json(new { status = result.Status, message = "Success", url = bankingApiResponse.PaymentUrl });
	}

	[HttpPut("RejectWithdrawRequest")]
	public async Task<IActionResult> RejectWithdrawRequest([FromBody] int id)
	{
		var reject = await _depositWithdrawRepository.RejectWithdrawRequest(id);

		return Ok(reject);
	}
}
