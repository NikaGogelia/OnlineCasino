using OnlineCasino.Models;
using OnlineCasino.Models.RepositoryModels;

namespace OnlineCasino.Repository.IRepository;

public interface IDepositWithdrawRepository
{
	Task<IEnumerable<RegisteredTransactionRequest>> GetRegisteredTransactionRequests();
	Task<DepositResponse> RegisterDeposit(string userId, decimal amount);
	Task<int> RegisterWithdraw(string userId, decimal amount);
	Task<string> RejectWithdrawRequest(int transactionRequestId);
}
