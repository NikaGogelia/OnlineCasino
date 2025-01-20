using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepositController : ControllerBase
	{
		private readonly IDepositService _depositService;

		public DepositController(IDepositService depositService)
		{
			_depositService = depositService;
		}

		[HttpPost("ProcessDeposit")]
		public async Task<IActionResult> ProcessDeposit([FromBody] DepositRequest request)
		{
			var result = await _depositService.ProcessDepositRequest(request);

			return Ok(result);
		}

		[HttpPost("CompleteDepositSendToCallback")]
		public async Task<IActionResult> CompleteDepositSendToCallback([FromBody] CallbackRequest request)
		{
			var response = await _depositService.SendRequestToCallback(request);

			return Ok(response);
		}
	}
}
