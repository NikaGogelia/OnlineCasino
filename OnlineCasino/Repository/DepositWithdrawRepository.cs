using Dapper;
using Microsoft.Data.SqlClient;
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
}
