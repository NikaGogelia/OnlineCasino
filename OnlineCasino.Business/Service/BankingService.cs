using BankApi;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineCasino.Models.Banking;
using OnlineCasino.Service.IService;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineCasino.Service;

public class BankingService : IBankingService
{
	private readonly AppSettings _appSettings;

	public BankingService(IOptions<AppSettings> appSettings)
	{
		_appSettings = appSettings.Value;
	}

	public async Task<BankingDepositResponse> SendDepositRequestAsync(BankingDepositRequest request)
	{
		string apiUrl = $"{_appSettings.BankApiUrl}/api/Deposit";

		using (HttpClient client = new HttpClient())
		{
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var settings = new JsonSerializerSettings
			{
				Converters = new List<JsonConverter> { new DecimalJsonConverter() },
				Culture = CultureInfo.InvariantCulture
			};

			var json = JsonConvert.SerializeObject(request, settings);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(apiUrl, content);

			var responseString = await response.Content.ReadAsStringAsync();

			var bankingDepositResponse = JsonConvert.DeserializeObject<BankingDepositResponse>(responseString);

			return bankingDepositResponse;
		}
	}

	public async Task<BankingWithdrawResponse> SendWithdrawRequestAsync(BankingWithdrawRequest request)
	{
		string apiUrl = $"{_appSettings.BankApiUrl}/api/Withdraw";

		using (HttpClient client = new HttpClient())
		{
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var json = JsonConvert.SerializeObject(request);
			HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await client.PostAsync(apiUrl, content);

			var responseString = await response.Content.ReadAsStringAsync();

			var bankingWithdrawResponse = JsonConvert.DeserializeObject<BankingWithdrawResponse>(responseString);

			return bankingWithdrawResponse;
		}
	}
}
