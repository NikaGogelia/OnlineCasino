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

		return await db.QuerySingleOrDefaultAsync<GetBalanceResponse>(
			sql,
			new
			{
				token = request.Token,
				gameId = request.GameId,
				productId = request.ProductId,
				currencyId = request.CurrencyId
			},
			commandType: CommandType.StoredProcedure) ?? new GetBalanceResponse { Status = -5 };
	}

	public async Task<GetPlayerInfoResponse> GetPlayerInformationWithToken(GetPlayerInfoRequest request)
	{
		string sql = "dbo.GetPlayerInfo";

		return await db.QuerySingleOrDefaultAsync<GetPlayerInfoResponse>(
			sql, 
			new { token = request.Token }, 
			commandType: CommandType.StoredProcedure) ?? new GetPlayerInfoResponse { Status = -5 };
	}
}
