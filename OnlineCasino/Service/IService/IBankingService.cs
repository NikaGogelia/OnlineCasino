using OnlineCasino.Models.Banking;

namespace OnlineCasino.Service.IService;

public interface IBankingService
{
	Task<BankingDepositResponse> SendDepositRequestAsync(BankingDepositRequest request);
	Task<BankingWithdrawResponse> SendWithdrawRequestAsync(BankingWithdrawRequest request);
}
