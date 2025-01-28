namespace OnlineCasinoAPI.Models.RequestModels;

public class BetRequest
{
	public string Token { get; set; }
	public decimal Amount { get; set; }
	public int BetTypeId { get; set; }
	public int GameId { get; set; }
	public int ProductId { get; set; }
	public int RoundId { get; set; }
	public int CurrencyId { get; set; }
}
