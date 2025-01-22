using BankApi.Enums;
using BankApi.Models;
using BankApi.Service.IService;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace BankApi.Service;

public class WithdrawService : IWithdrawService
{
	private readonly AppSettings _appSettings;

	public WithdrawService(IOptions<AppSettings> appSettings)
	{
		_appSettings = appSettings.Value;
	}

	public async Task<WithdrawResponse> ProcessWithdrawRequest(WithdrawRequest request)
	{
		if (!ValidateHash(request))
		{
			return new WithdrawResponse { Status = Status.Rejected.ToString(), Message = "Invalid Hash!" };
		}

		bool isAmountEven = request.Amount % 2 == 0;

		if (!isAmountEven)
		{
			return new WithdrawResponse { Status = Status.Rejected.ToString(), Message = "Amount Should Be Even!" };
		}

		return new WithdrawResponse { Status = Status.Success.ToString(), Message = "Withdraw Request Was Successful!" };
	}

	public async Task<CallbackResponse> SendRequestToCallback(CallbackRequest request)
	{
		string apiUrl = $"{_appSettings.CallbackApiUrl}/CompleteWithdraw";

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

	private bool ValidateHash(WithdrawRequest request)
	{
		string concatenatedParams = $"{request.Amount}{request.MerchantId}{request.TransactionId}{request.UsersAccountNumber}{request.UsersFullName}{_appSettings.SecretKey}";
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
