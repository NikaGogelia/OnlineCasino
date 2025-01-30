using OnlineCasinoAPI.Models.RequestModels;

namespace OnlineCasinoAPI.Repository.IRepository;

public interface IWinRepository
{
	Task<WinResponse> RegisterWin(WinRequest request);
	Task<WinResponse> ChangeWin(ChangeWinRequest request);
}
