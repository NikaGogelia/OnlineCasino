using Dapper;
using Microsoft.Data.SqlClient;
using OnlineCasinoAPI.Models.RequestModels;
using OnlineCasinoAPI.Repository.IRepository;
using System.Data;

namespace OnlineCasinoAPI.Repository;

public class PlayerRepository : IPlayerRepository
{
	private IDbConnection db;

	public PlayerRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public async Task<GetBalanceResponse> GetBalanceForPlayer(GetBalanceRequest request)
	{
		string sql = "dbo.GetBalance";

		DynamicParameters parameters = new DynamicParameters();
		parameters.Add("token", request.Token);
		parameters.Add("gameId", request.GameId);
		parameters.Add("productId", request.ProductId);
		parameters.Add("currencyId", request.CurrencyId);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("currentBalance", dbType: DbType.Decimal, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var currentBalance = parameters.Get<decimal?>("currentBalance");

		return new GetBalanceResponse
		{
			Status = status,
			CurrentBalance = currentBalance
		};
	}

	public async Task<GetPlayerInfoResponse> GetPlayerInformationWithToken(GetPlayerInfoRequest request)
	{
		string sql = "dbo.GetPlayerInfo";

		DynamicParameters parameters = new DynamicParameters();
		parameters.Add("token", request.Token);
		parameters.Add("userId", dbType: DbType.String, size: 450, direction: ParameterDirection.Output);
		parameters.Add("userName", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
		parameters.Add("email", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
		parameters.Add("currency", dbType: DbType.String, size: 3, direction: ParameterDirection.Output);
		parameters.Add("currentBalance", dbType: DbType.Decimal, direction: ParameterDirection.Output);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);

		var userId = parameters.Get<string>("userId");
		var userName = parameters.Get<string>("userName");
		var email = parameters.Get<string>("email");
		var currency = parameters.Get<string>("currency");
		var currentBalance = parameters.Get<decimal?>("currentBalance");
		var status = parameters.Get<int>("status");

		return new GetPlayerInfoResponse
		{
			Status = status,
			UserId = userId,
			UserName = userName,
			Email = email,
			Currency = currency,
			CurrentBalance = currentBalance,
		};
	}
}
