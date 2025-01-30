namespace OnlineCasinoAPI.Models.RequestModels;

public class GetBalanceRequest
{
	public string Token { get; set; }
	public int GameId { get; set; }
	public int ProductId { get; set; }
	public int CurrencyId { get; set; }
}
