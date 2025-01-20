using Dapper;
using Microsoft.Data.SqlClient;
using OnlineCasino.Models.RepositoryModels;
using OnlineCasino.Repository.IRepository;
using System.Data;

namespace OnlineCasino.Repository;

public class CallbackRepository : ICallbackRepository
{
	private IDbConnection db;

	public CallbackRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public async Task<CallbackResponse> CompleteDeposit(int transactionId, decimal amount, string status)
	{
		string sql = "dbo.CompleteDepositTransaction";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("transactionId", transactionId);
		parameters.Add("amount", amount);
		parameters.Add("status", status);
		parameters.Add("transactionStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var transactionStatus = parameters.Get<int>("transactionStatus");
		var message = parameters.Get<string>("message");

		return new CallbackResponse { TransactionStatus = transactionStatus, Message = message };
	}

	public async Task<CallbackResponse> CompleteWithdraw(int transactionId, decimal amount, string status)
	{
		string sql = "dbo.CompleteWithdrawTransaction";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("transactionId", transactionId);
		parameters.Add("amount", amount);
		parameters.Add("status", status);
		parameters.Add("transactionStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var transactionStatus = parameters.Get<int>("transactionStatus");
		var message = parameters.Get<string>("message");

		return new CallbackResponse { TransactionStatus = transactionStatus, Message = message };
	}
}
