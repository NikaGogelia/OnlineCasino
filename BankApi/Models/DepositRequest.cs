namespace BankApi.Models;

public class DepositRequest
{
	public int TransactionId { get; set; }
	public decimal Amount { get; set; }
	public string MerchantId { get; set; }
	public string Hash { get; set; }
}
