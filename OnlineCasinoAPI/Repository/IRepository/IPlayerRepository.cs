using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Repository.IRepository;

public interface IPlayerRepository
{
	Task<GetBalanceResponse> GetBalanceForPlayer(GetBalanceRequest request);
	Task<GetPlayerInfoResponse> GetPlayerInformationWithToken(GetPlayerInfoRequest request);
}
