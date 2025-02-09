﻿namespace OnlineCasinoAPI.Models.RequestModels;

public class CancelBetRequest
{
	public string Token { get; set; }
	public decimal Amount { get; set; }
	public int? TransactionId { get; set; }
	public int BetTypeId { get; set; }
	public int GameId { get; set; }
	public int ProductId { get; set; }
	public int RoundId { get; set; }
	public string? Hash { get; set; }
	public int CurrencyId { get; set; }
	public int BetTransactionId { get; set; }
}
