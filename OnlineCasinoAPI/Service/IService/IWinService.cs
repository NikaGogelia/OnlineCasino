using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Service.IService;

public interface IWinService
{
	Task<WinResponse> Win(WinRequest betRequest);
}
