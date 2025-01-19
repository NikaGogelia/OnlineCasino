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
		public async Task<IActionResult> ProcessDeposit([FromBody] WithdrawRequest request)
		{
			var result = await _withdrawService.ProcessWithdrawRequest(request);

			return Ok(result);
		}
	}
}
