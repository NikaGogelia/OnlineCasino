using BankApi.Enums;
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

		[HttpPost]
		public async Task<IActionResult> ProcessDeposit([FromBody] DepositRequest request)
		{
			var result = await _depositService.ProcessDepositRequest(request);

			return Ok(result);
		}
	}
}
