namespace OnlineCasinoAPI.Models.RequestModels;

public class GetPlayerInfoResponse
{
	public int Status { get; set; }
	public string? UserId { get; set; }
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? Currency { get; set; }
	public decimal? CurrentBalance { get; set; }
}
