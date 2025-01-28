using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Repository.IRepository;

public interface IBetRepository
{
	Task<BetResponse> RegisterBet(BetRequest request);
}
