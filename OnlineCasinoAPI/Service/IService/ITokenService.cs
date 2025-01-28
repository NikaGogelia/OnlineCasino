using OnlineCasinoAPI.Models;

namespace OnlineCasinoAPI.Services.IService;

public interface ITokenService
{
	Task<TokenResponse> AuthToken(string publicToken);
}
