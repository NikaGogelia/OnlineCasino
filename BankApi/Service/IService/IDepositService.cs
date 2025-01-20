using BankApi.Models;

namespace BankApi.Service.IService;

public interface IDepositService
{
	Task<DepositResponse> ProcessDepositRequest(DepositRequest request);
	Task<CallbackResponse> SendRequestToCallback(CallbackRequest request);
}
