namespace OnlineCasinoAPI.Models.RequestModels;

public class WinRequest
{
	public string Token { get; set; }
	public decimal Amount { get; set; }
	public int? TransactionId { get; set; }
	public int WinTypeId { get; set; }
	public int GameId { get; set; }
	public int ProductId { get; set; }
	public int RoundId { get; set; }
	public string? Hash { get; set; }
	public int CurrencyId { get; set; }
	public int? CampaignId { get; set; }
	public string? CampaignName { get; set; }
}
