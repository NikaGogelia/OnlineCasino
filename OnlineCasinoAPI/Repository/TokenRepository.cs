using Dapper;
using Microsoft.Data.SqlClient;
using OnlineCasinoAPI.Models;
using OnlineCasinoAPI.Repository.IRepository;
using System.Data;


namespace OnlineCasinoAPI.Repository;

public class TokenRepository : ITokenRepository
{
	private IDbConnection db;

	public TokenRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}


	public async Task<TokenResponse> GeneratePrivateToken(string publicToken, Guid privateToken)
	{
		string sql = "dbo.CreatePrivateToken";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("publicToken", publicToken);
		parameters.Add("privateToken", privateToken);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("token", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var token = parameters.Get<string>("token");

		return new TokenResponse { Status = status, PrivateToken = token };
	}
}
