namespace OnlineCasinoAPI.Models.RequestModels;

public class GetPlayerInfoRequest
{
	public required string Token { get; set; }
	public string? Hash { get; set; }
}
