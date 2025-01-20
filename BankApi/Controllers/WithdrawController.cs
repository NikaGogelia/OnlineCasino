using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WithdrawController : ControllerBase
	{
		private readonly IWithdrawService _withdrawService;

		public WithdrawController(IWithdrawService withdrawService)
		{
			_withdrawService = withdrawService;
		}

		[HttpPost]
		public async Task<IActionResult> ProcessWithdraw([FromBody] WithdrawRequest request)
		{
			var result = await _withdrawService.ProcessWithdrawRequest(request);

			var callbackRequest = new CallbackRequest { Amount = request.Amount, TransactionId = request.TransactionId, Status = result.Status };

			var callbackResponse = await _withdrawService.SendRequestToCallback(callbackRequest);

			return Ok(result);
		}
	}
}
