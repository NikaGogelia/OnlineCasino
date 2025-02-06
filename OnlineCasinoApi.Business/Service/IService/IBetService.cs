using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Service.IService;

public interface IBetService
{
	Task<BetResponse> Bet(BetRequest request);
	Task<BetResponse> Cancel(CancelBetRequest request);
}
