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

	public async Task<DataForBankingWithdrawService> GetDataForBankingWithdrawService(int transactionId)
	{
		string sql = "SELECT dw.Amount, u.UserName FROM dbo.DepositWithdrawRequests AS dw INNER JOIN dbo.AspNetUsers AS u ON dw.UserId = u.Id WHERE dw.Id = @Id";

		var result = await db.QueryAsync<DataForBankingWithdrawService>(sql, new { @Id = transactionId });

		return result.Single();
	}

	public async Task<RegisteredTransactionRequest> GetRegisteredTransactionRequest(int transactionId)
	{
		string sql =
			"SELECT dpw.Id, u.UserName, dpw.TransactionType, dpw.Amount, dpw.Status FROM dbo.DepositWithdrawRequests AS dpw " +
			"INNER JOIN dbo.AspNetUsers AS u " +
			"ON dpw.UserId = u.Id " +
			"WHERE dpw.Id = @Id";

		var result = await db.QueryAsync<RegisteredTransactionRequest>(sql, new { @Id = transactionId });

		return result.Single();
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
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("transactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var transactionId = parameters.Get<int>("transactionId");

		return new DepositResponse { Status = status, TransactionId = transactionId };
	}

	public async Task<WithdrawResponse> RegisterWithdraw(string userId, decimal amount)
	{
		string sql = "dbo.RegisterWithdraw";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("userId", userId);
		parameters.Add("amount", amount);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("transactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var transactionId = parameters.Get<int>("transactionId");
		var message = parameters.Get<string>("message");

		return new WithdrawResponse { Status = status, TransactioId = transactionId, Message = message };
	}

	public async Task<string> RejectWithdrawRequest(int transactionRequestId)
	{
		string sql = "dbo.RejectWithdrawRequest";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("transactionId", transactionRequestId);
		parameters.Add("message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var message = parameters.Get<string>("message");

		return message;
	}
}
