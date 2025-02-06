using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OnlineCasino.Models.RepositoryModels;
using OnlineCasino.Repository.IRepository;
using System.Data;

namespace OnlineCasino.Repository;

public class TransactionsRepository : ITransactionsRepository
{
	private IDbConnection db;

	public TransactionsRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public async Task<IEnumerable<Transaction>> GetAllTransactionsForCurrentUser(string userId)
	{
		string sql =
			"SELECT t.Id, u.UserName, t.Amount, t.Status, t.TransactionType ,t.CreatedAt FROM dbo.Transactions AS t " +
			"INNER JOIN dbo.AspNetUsers AS u " +
			"ON t.UserId = u.Id " +
			"WHERE t.UserId = @Id";

		var result = await db.QueryAsync<Transaction>(sql, new { @Id = userId });

		return result;
	}
}
