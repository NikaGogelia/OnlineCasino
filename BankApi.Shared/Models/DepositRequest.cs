namespace BankApi.Models;

public class DepositRequest
{
	public int TransactionId { get; set; }
	public decimal Amount { get; set; }
	public required string MerchantId { get; set; }
	public required string Hash { get; set; }
}
