namespace OnlineCasino.Models.Banking
{
	public class BankingDepositResponse
	{
		public string PaymentUrl { get; set; }
		public required string Status { get; set; }
		public string Message { get; set; }
	}
}
