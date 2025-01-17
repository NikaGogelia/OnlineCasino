using BankApi.Enums;
using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace BankApi.Service;

public class DepositService : IDepositService
{
	private readonly string _secretKey;

	public DepositService(IOptions<AppSettings> appSettings)
	{
		_secretKey = appSettings.Value.SecretKey;
	}

	public async Task<DepositResponse> ProcessDepositRequest(DepositRequest request)
	{
		if (!ValidateHash(request))
		{
			return new DepositResponse { Status = Status.Rejected.ToString(), Message = "Invalid Hash!" };
		}

		bool isAmountEven = request.Amount % 2 == 0;

		if (!isAmountEven)
		{
			return new DepositResponse { Status = Status.Rejected.ToString(), Message = "Amount Should Be Even!" };
		}

		string paymentUrl = "/payment/finalize?" + request.TransactionId;

		return new DepositResponse { Status = Status.Success.ToString(), PaymentUrl = paymentUrl };
	}

	private bool ValidateHash(DepositRequest request)
	{
		string concatenatedParams = $"{request.Amount}{request.MerchantId}{request.TransactionId}{_secretKey}";
		string calculatedHash = CalculateSha256Hash(concatenatedParams);

		return calculatedHash.Equals(request.Hash, StringComparison.OrdinalIgnoreCase);
	}

	private string CalculateSha256Hash(string input)
	{
		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}
	}
}
