using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Repository.IRepository;
using System.Data;

namespace OnlineCasinoAPI.Repository;

public class BetRepository : IBetRepository
{
	private IDbConnection db;

	public BetRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public async Task<BetResponse> RegisterBet(BetRequest request)
	{
		string sql = "dbo.RegisterBet";

		DynamicParameters parameters = new DynamicParameters();
		parameters.Add("token", request.Token);
		parameters.Add("amount", request.Amount);
		parameters.Add("betTypeId", request.BetTypeId);
		parameters.Add("gameId", request.GameId);
		parameters.Add("productId", request.ProductId);
		parameters.Add("roundId", request.RoundId);
		parameters.Add("currencyId", request.CurrencyId);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("transactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("currentBalance", dbType: DbType.Decimal, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var transactionId = parameters.Get<int?>("transactionId");
		var currentBalance = parameters.Get<decimal?>("currentBalance");

		return new BetResponse
		{
			Status = status,
			TransactionId = transactionId,
			CurrentBalance = currentBalance
		};
	}

	public async Task<BetResponse> CancelBet(CancelBetRequest request)
	{
		string sql = "dbo.CancelBet";

		DynamicParameters parameters = new DynamicParameters();
		parameters.Add("token", request.Token);
		parameters.Add("amount", request.Amount);
		parameters.Add("betTypeId", request.BetTypeId);
		parameters.Add("gameId", request.GameId);
		parameters.Add("productId", request.ProductId);
		parameters.Add("roundId", request.RoundId);
		parameters.Add("currencyId", request.CurrencyId);
		parameters.Add("betTransactionId", request.BetTransactionId);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("transactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("currentBalance", dbType: DbType.Decimal, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var transactionId = parameters.Get<int?>("transactionId");
		var currentBalance = parameters.Get<decimal?>("currentBalance");

		return new BetResponse
		{
			Status = status,
			TransactionId = transactionId,
			CurrentBalance = currentBalance
		};
	}
}
