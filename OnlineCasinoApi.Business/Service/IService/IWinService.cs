using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Service.IService;

public interface IWinService
{
	Task<WinResponse> Win(WinRequest request);
	Task<WinResponse> Change(ChangeWinRequest request);
}
