using BankApi.Models;

namespace BankApi.Service.IService;

public interface IWithdrawService
{
	Task<WithdrawResponse> ProcessWithdrawRequest(WithdrawRequest request);
}
