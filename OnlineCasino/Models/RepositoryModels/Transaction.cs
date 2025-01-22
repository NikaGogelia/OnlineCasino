namespace OnlineCasino.Models.RepositoryModels;

public class Transaction
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public decimal Amount { get; set; }
	public string Status { get; set; }
	public string TransactionType { get; set; }
	public DateTime CreatedAt { get; set; }
}
