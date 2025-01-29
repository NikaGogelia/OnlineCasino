using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Service.IService;

public interface IBetService
{
	Task<BetResponse> Bet(BetRequest betRequest);
	Task<BetResponse> Cancel(CancelBetRequest betRequest);
}
