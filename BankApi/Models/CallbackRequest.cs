﻿namespace BankApi.Models;

public class CallbackRequest
{
	public int TransactionId { get; set; }
	public decimal Amount { get; set; }
	public string Status { get; set; }
}
