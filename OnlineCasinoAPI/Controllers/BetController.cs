using Microsoft.AspNetCore.Mvc;
using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BetController : ControllerBase
	{
		private readonly IBetService _betService;

		public BetController(IBetService betService)
		{
			_betService = betService;
		}

		[HttpPost]
		public async Task<IActionResult> RegisterBet([FromBody] BetRequest request)
		{
			var response = await _betService.Bet(request);

			return response.Status switch
			{
				1 => Ok(new
				{
					StatusCode = 200,
					Data = new
					{
						TransactionId = response.TransactionId?.ToString(),
						CurrentBalance = response.CurrentBalance
					}
				}),
				-1 => StatusCode(402, new { StatusCode = 402, Message = "Insufficient Balance!" }),
				-2 => StatusCode(400, new { StatusCode = 400, Message = "Incorrect Currency!" }),
				-3 => StatusCode(407, new { StatusCode = 407, Message = "Invalid Amount!" }),
				-4 => StatusCode(411, new { StatusCode = 411, Message = "Invalid Request!" }),
				-5 => StatusCode(401, new { StatusCode = 401, Message = "Inactive Token!" }),
				_ => StatusCode(500, new { StatusCode = 500 })
			};
		}
	}
}
