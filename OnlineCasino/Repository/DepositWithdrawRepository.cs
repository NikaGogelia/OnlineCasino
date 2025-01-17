using Dapper;
using Microsoft.Data.SqlClient;
using OnlineCasino.Models;
using OnlineCasino.Models.RepositoryModels;
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

	public async Task<IEnumerable<RegisteredTransactionRequest>> GetRegisteredTransactionRequests()
	{
		string sql =
			"SELECT dpw.Id, u.UserName, dpw.TransactionType, dpw.Amount, dpw.Status FROM dbo.DepositWithdrawRequests AS dpw " +
			"INNER JOIN dbo.AspNetUsers AS u " +
			"ON dpw.UserId = u.Id " +
			"WHERE dpw.TransactionType = 'withdraw'";

		var result = await db.QueryAsync<RegisteredTransactionRequest>(sql);

		return result;
	}

	public async Task<DepositResponse> RegisterDeposit(string userId, decimal amount)
	{
		string sql = "dbo.RegisterDeposit";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("userId", userId);
		parameters.Add("amount", amount);
		parameters.Add("transactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var transactionId = parameters.Get<int>("transactionId");

		return new DepositResponse { Status = status, TransactionId = transactionId };
	}

	public Task<int> RegisterWithdraw(string userId, decimal amount)
	{
		throw new NotImplementedException();
	}

	public async Task<string> RejectWithdrawRequest(int transactionRequestId)
	{
		string sql = "UPDATE dbo.DepositWithdrawRequests SET Status = 'rejected' WHERE Id = @Id AND TransactionType = 'withdraw'";

		await db.ExecuteAsync(sql, new { @Id = transactionRequestId });

		return "Withdraw Request Was Rejected!";
	}
}
