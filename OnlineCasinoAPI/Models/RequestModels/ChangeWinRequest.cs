namespace OnlineCasinoAPI.Models.RequestModels;

public class ChangeWinRequest
{
	public string Token { get; set; }
	public decimal Amount { get; set; }
	public decimal PreviousAmount { get; set; }
	public int PreviousTransactionId { get; set; }
	public int ChangeWinTypeId { get; set; }
	public int GameId { get; set; }
	public int ProductId { get; set; }
	public int RoundId { get; set; }
	public int CurrencyId { get; set; }
}
