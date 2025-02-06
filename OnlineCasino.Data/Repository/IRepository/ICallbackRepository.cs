using OnlineCasino.Models.RepositoryModels;

namespace OnlineCasino.Repository.IRepository;

public interface ICallbackRepository
{
	Task<CallbackResponse> CompleteDeposit(int transactionId, decimal amount, string status);
	Task<CallbackResponse> CompleteWithdraw(int transactionId, decimal amount, string status);
}
