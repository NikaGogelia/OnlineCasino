namespace OnlineCasino.Models.RepositoryModels;

public class TokenResponse
{
	public required int Status { get; set; }
	public required string Message { get; set; }
	public required string PublicToken { get; set; }
}
