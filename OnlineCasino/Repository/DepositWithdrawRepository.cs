using Dapper;
using Microsoft.Data.SqlClient;
using NuGet.Protocol;
using OnlineCasino.Models;
using OnlineCasino.Repository.IRepository;
using System.Data;

namespace OnlineCasino.Repository;

public class DepositWithdrawRepository : IDepositWithdrawRepository
{
	private IDbConnection db;

	public DepositWithdrawRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public async Task<string> TransactionRequest(string userId, decimal amount, string transactionType)
	{
		string sql = "INSERT INTO DepositWithdrawRequests(UserId, TransactionType, Amount, Status) VALUES(@UserId, @TransactionType, @Amount, @Status)";

		try
		{
			await db.ExecuteAsync(sql, new { UserId = userId, TransactionType = transactionType, Amount = amount, Status = "pending" });

			return $"Transaction request ({transactionType}) registered successfully.";
		}
		catch (Exception ex)
		{
			return $"An error occurred: {ex.Message}";
		}
	}

	public async Task<IEnumerable<RegisteredTransactionRequest>> GetRegisteredTransactionRequests()
	{
		string sql =
			"SELECT dpw.Id, u.UserName, dpw.TransactionType, dpw.Amount, dpw.Status FROM dbo.DepositWithdrawRequests AS dpw " +
			"INNER JOIN dbo.AspNetUsers AS u " +
			"ON dpw.UserId = u.Id";

		var result = await db.QueryAsync<RegisteredTransactionRequest>(sql);

		return result;
	}

	public async Task<string> RejectWithdrawRequest(int transactionRequestId)
	{
		string sql = "UPDATE dbo.DepositWithdrawRequests SET Status = 'rejected' WHERE Id = @Id AND TransactionType = 'withdraw'";

		var updateWithdrawStatus = await db.ExecuteAsync(sql, new { @Id = transactionRequestId });

		return "Withdraw Request Was Rejected!";
	}
}
