using OnlineCasino.Models.RepositoryModels;

namespace OnlineCasino.Repository.IRepository;

public interface ICallbackRepository
{
	Task<DepositCallbackResponse> CompleteDeposit(int transactionId, decimal amount, string status);
}
