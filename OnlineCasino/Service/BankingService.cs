using Newtonsoft.Json;
using OnlineCasino.Models.Banking;
using OnlineCasino.Service.IService;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineCasino.Service;

public class BankingService : IBankingService
{
	public async Task<BankingDepositResponse> SendDepositRequestAsync(BankingDepositRequest request)
	{
		string apiUrl = "https://localhost:7213/api/Deposit";

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
}
