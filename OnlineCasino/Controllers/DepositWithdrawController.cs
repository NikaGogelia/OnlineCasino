﻿using BankApi;
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

		var registeredDeposit = await _depositWithdrawRepository.RegisterDeposit(userId, request.Amount);

		if (registeredDeposit.Status != 1)
		{
			return Json(new { status = registeredDeposit.Status, message = "Something went wrong!" });
		}

		var apiRequest = new BankingDepositRequest
		{
			Amount = request.Amount,
			TransactionId = registeredDeposit.TransactionId,
			MerchantId = _appSettings.MerchantId,
			Hash = HashDepositService.GenerateHash(request.Amount, _appSettings.MerchantId, registeredDeposit.TransactionId, _appSettings.SecretKey)
		};

		var bankingApiResponse = await _bankingService.SendDepositRequestAsync(apiRequest);

		return Json(new { status = registeredDeposit.Status, message = bankingApiResponse.Status, url = bankingApiResponse.PaymentUrl });
	}

	[HttpPost("RegisterWithdraw")]
	public async Task<IActionResult> RegisterWithdraw([FromBody] TransactionRequest request)
	{
		var userId = _userManager.GetUserId(User);

		if (userId == null)
		{
			return Json(new { status = 0, message = "user not found" });
		}

		var registeredDeposit = await _depositWithdrawRepository.RegisterWithdraw(userId, request.Amount);

		return Json(new { status = registeredDeposit.Status, transactionId = registeredDeposit.TransactioId, message = registeredDeposit.Message });
	}

	[HttpPost("ApproveWithdrawRequest")]
	public async Task<IActionResult> ApproveWithdrawRequest([FromBody] int id)
	{
		var getDataForBankingService = await _depositWithdrawRepository.GetDataForBankingWithdrawService(id);

		var usersAccountNumber = 1234567;

		var apiRequest = new BankingWithdrawRequest
		{
			TransactionId = id,
			Amount = getDataForBankingService.Amount,
			MerchantId = _appSettings.MerchantId,
			UsersAccountNumber = usersAccountNumber,
			UsersFullName = getDataForBankingService.UserName,
			Hash = HashWithdrawService.GenerateHash(getDataForBankingService.Amount, _appSettings.MerchantId, id, usersAccountNumber, getDataForBankingService.UserName, _appSettings.SecretKey)
		};

		var approve = await _bankingService.SendWithdrawRequestAsync(apiRequest);

		return Ok(approve);
	}

	[HttpPut("RejectWithdrawRequest")]
	public async Task<IActionResult> RejectWithdrawRequest([FromBody] int id)
	{
		var reject = await _depositWithdrawRepository.RejectWithdrawRequest(id);

		return Ok(reject);
	}
}
