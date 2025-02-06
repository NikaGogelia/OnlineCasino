namespace OnlineCasinoAPI.Models.RequestModels;

public class WinResponse
{
	public int Status { get; set; }
	public int? TransactionId { get; set; }
	public decimal? CurrentBalance { get; set; }
}
