using OnlineCasinoAPI.Models;

namespace OnlineCasinoAPI.Repository.IRepository;

public interface ITokenRepository
{
	Task<TokenResponse> GeneratePrivateToken(string publicToken, Guid privateToken);
}
