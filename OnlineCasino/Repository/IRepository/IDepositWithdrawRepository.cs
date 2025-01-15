using OnlineCasino.Models;

namespace OnlineCasino.Repository.IRepository;

public interface IDepositWithdrawRepository
{
	Task<string> TransactionRequest(string userId, decimal amount, string transactionType);
	Task<IEnumerable<RegisteredTransactionRequest>> GetRegisteredTransactionRequests();
	Task<string> RejectWithdrawRequest(int transactionRequestId);
}
