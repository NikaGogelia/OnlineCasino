using Dapper;
using Microsoft.Data.SqlClient;
using OnlineCasino.Repository.IRepository;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data.Common;
using OnlineCasino.Models;

namespace OnlineCasino.Repository;

public class WalletRepository : IWalletRepository
{
	private IDbConnection db;

	public WalletRepository(IConfiguration configuration)
	{
		db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
	}

	public void CreateWallet(string userId, int currency)
	{
		string sql = "INSERT INTO Wallet(UserId, CurrentBallance, Currency) VALUES (@UserId, @CurrentBallance, @Currency)";

		db.Execute(sql, new { @UserId = userId, @CurrentBallance = 0, @Currency = currency });
	}

	public async Task<Balance> GetWalletBalance(string userId)
	{
		string sql = "SELECT w.CurrentBallance, c.CurrencyName FROM Wallet AS w INNER JOIN Currency AS c ON w.Currency = c.Id WHERE w.UserId = @UserId";

		var walletBalance = await db.QueryAsync<Balance>(sql, new { UserId = userId });

		return walletBalance.Single();
	}
}
