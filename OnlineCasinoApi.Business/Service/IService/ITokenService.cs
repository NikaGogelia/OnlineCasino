using OnlineCasinoAPI.Models;

namespace OnlineCasinoAPI.Service.IService;

public interface ITokenService
{
	Task<TokenResponse> AuthToken(string publicToken);
}
