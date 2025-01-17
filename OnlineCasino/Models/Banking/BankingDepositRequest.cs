namespace OnlineCasino.Models.Banking;

public class BankingDepositRequest
{
	public int TransactionId { get; set; }
	public decimal Amount { get; set; }
	public required string MerchantId { get; set; }
	public required string Hash { get; set; }
}
