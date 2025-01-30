using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Service.IService;

public interface IPlayerService
{
	Task<GetBalanceResponse> GetPlayerBalance(GetBalanceRequest request);
	Task<GetPlayerInfoResponse> GetPlayerInfo(GetPlayerInfoRequest request);
}
