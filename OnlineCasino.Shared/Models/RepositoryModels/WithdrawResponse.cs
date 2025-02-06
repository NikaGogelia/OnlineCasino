namespace OnlineCasino.Models.RepositoryModels;

public class WithdrawResponse
{
	public int Status { get; set; }
	public int? TransactioId { get; set; }
	public string? Message { get; set; }
}
