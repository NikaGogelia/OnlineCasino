using OnlineCasino.Models.RepositoryModels;

namespace OnlineCasino.Repository.IRepository;

public interface IDepositWithdrawRepository
{
	Task<DataForBankingWithdrawService> GetDataForBankingWithdrawService(int transactionId);
	Task<RegisteredTransactionRequest> GetRegisteredTransactionRequest(int transactionId);
	Task<IEnumerable<RegisteredTransactionRequest>> GetRegisteredTransactionRequests();
	Task<DepositResponse> RegisterDeposit(string userId, decimal amount);
	Task<WithdrawResponse> RegisterWithdraw(string userId, decimal amount);
	Task<string> RejectWithdrawRequest(int transactionRequestId);
}
