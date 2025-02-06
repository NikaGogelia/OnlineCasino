using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Repository.IRepository;
using OnlineCasinoAPI.Service.IService;

namespace OnlineCasinoAPI.Service
{
	public class PlayerService : IPlayerService
	{
		private readonly IPlayerRepository _playerRepository;

		public PlayerService(IPlayerRepository playerRepository)
		{
			_playerRepository = playerRepository;
		}

		public async Task<GetBalanceResponse> GetPlayerBalance(GetBalanceRequest request)
		{
			return await _playerRepository.GetBalanceForPlayer(request);
		}

		public async Task<GetPlayerInfoResponse> GetPlayerInfo(GetPlayerInfoRequest request)
		{
			return await _playerRepository.GetPlayerInformationWithToken(request);
		}
	}
}
