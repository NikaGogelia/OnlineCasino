using BankApi.Enums;

namespace BankApi.Models;

public class DepositResponse
{
	public Status Status { get; set; } = Status.Rejected;
	public string PaymentUrl { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
}
