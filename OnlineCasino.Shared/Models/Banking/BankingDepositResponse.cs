namespace OnlineCasino.Models.Banking;

public class BankingDepositResponse
{
	public string Status { get; set; } = string.Empty;
	public string PaymentUrl { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
}
