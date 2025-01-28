using OnlineCasinoAPI.Models;
using OnlineCasinoAPI.Repository.IRepository;
using OnlineCasinoAPI.Services.IService;

namespace OnlineCasinoAPI.Services;

public class TokenService : ITokenService
{
	private readonly ITokenRepository _tokenRepository;

	public TokenService(ITokenRepository tokenRepository)
	{
		_tokenRepository = tokenRepository;
	}

	public async Task<TokenResponse> AuthToken(string publicToken)
	{
		Guid token = Guid.NewGuid();

		var createToken = await _tokenRepository.GeneratePrivateToken(publicToken, token);

		return createToken;
	}
}
