namespace OnlineCasino.Models.Banking;

public class BankingWithdrawRequest
{
	public int TransactionId { get; set; }
	public decimal Amount { get; set; }
	public required string MerchantId { get; set; }
	public required int UsersAccountNumber { get; set; }
	public required string UsersFullName { get; set; }
	public required string Hash { get; set; }
}
