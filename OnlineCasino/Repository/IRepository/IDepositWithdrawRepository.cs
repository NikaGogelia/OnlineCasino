namespace OnlineCasino.Repository.IRepository;

public interface IDepositWithdrawRepository
{
	Task<string> TransactionRequest(string userId, decimal amount, string transactionType);
}
