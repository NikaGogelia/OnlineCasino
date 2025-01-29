using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Repository.IRepository;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Service;

public class WinService : IWinService
{
	private readonly IWinRepository _winRepository;

	public WinService(IWinRepository winRepository)
	{
		_winRepository = winRepository;
	}

	public async Task<WinResponse> Win(WinRequest betRequest)
	{
		return await _winRepository.RegisterWin(betRequest);
	}
}
