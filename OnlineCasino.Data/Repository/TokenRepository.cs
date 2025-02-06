using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OnlineCasino.Models.RepositoryModels;
using OnlineCasino.Repository.IRepository;
using System.Data;

namespace OnlineCasino.Repository;

public class TokenRepository : ITokenRepository
{
	private IDbConnection db;

	public TokenRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public async Task<TokenResponse> CreatePublicToken(string userId, Guid token)
	{
		string sql = "dbo.CreatePublicToken";

		DynamicParameters parameters = new DynamicParameters();

		parameters.Add("userId", userId);
		parameters.Add("publicToken", token);
		parameters.Add("status", dbType: DbType.Int32, direction: ParameterDirection.Output);
		parameters.Add("message", dbType: DbType.String, size: 100, direction: ParameterDirection.Output);
		parameters.Add("token", dbType: DbType.String, size: 40, direction: ParameterDirection.Output);

		await db.ExecuteAsync(sql, param: parameters, commandType: CommandType.StoredProcedure);

		var status = parameters.Get<int>("status");
		var message = parameters.Get<string>("message");
		var publicToken = parameters.Get<string>("token");

		return new TokenResponse { Status = status, Message = message, PublicToken = publicToken };
	}
}
