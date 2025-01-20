using OnlineCasino.Models.RepositoryModels;

namespace OnlineCasino.Repository.IRepository;

public interface ITransactionsRepository
{
	Task<IEnumerable<Transaction>> GetAllTransactionsForCurrentUser(string userId);
}
