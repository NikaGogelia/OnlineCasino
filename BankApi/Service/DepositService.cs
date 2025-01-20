using BankApi.Enums;
using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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

		string paymentUrl = "";

		if (!isAmountEven)
		{
			paymentUrl = $"/FinalizePayment/Deposit/{request.TransactionId}/{Status.Rejected.ToString()}";
			return new DepositResponse { Status = Status.Rejected.ToString(), PaymentUrl = paymentUrl, Message = "Amount Should Be Even!" };
		}

		paymentUrl = $"/FinalizePayment/Deposit/{request.TransactionId}/{Status.Success.ToString()}";

		return new DepositResponse { Status = Status.Success.ToString(), PaymentUrl = paymentUrl, Message = "Deposit Transaction Success!" };
	}

	public async Task<CallbackResponse> SendRequestToCallback(CallbackRequest request)
	{
		string apiUrl = "http://localhost:5117/CompleteDeposit";

		using (HttpClient client = new HttpClient())
		{
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var json = JsonConvert.SerializeObject(request);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(apiUrl, content);

			var responseString = await response.Content.ReadAsStringAsync();

			var callbackResponse = JsonConvert.DeserializeObject<CallbackResponse>(responseString);

			return callbackResponse;
		}
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
