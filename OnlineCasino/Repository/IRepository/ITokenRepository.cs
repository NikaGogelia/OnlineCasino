using OnlineCasino.Models.RepositoryModels;

namespace OnlineCasino.Repository.IRepository;

public interface ITokenRepository
{
	Task<TokenResponse> CreatePublicToken(string userId, Guid token);
}
