using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Repository.IRepository;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Service;

public class BetService : IBetService
{
	private readonly IBetRepository _betRepository;

	public BetService(IBetRepository betRepository)
	{
		_betRepository = betRepository;
	}

	public async Task<BetResponse> Bet(BetRequest betRequest)
	{
		return await _betRepository.RegisterBet(betRequest);
	}
}
