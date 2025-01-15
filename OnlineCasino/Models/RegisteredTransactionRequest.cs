namespace OnlineCasino.Models;

public class RegisteredTransactionRequest
{
	public int Id { get; set; }
	public string UserName { get; set; }
	public string TransactionType { get; set; }
	public decimal Amount { get; set; }
	public string Status { get; set; }
}
