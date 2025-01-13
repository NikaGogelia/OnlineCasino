using OnlineCasino.Models;

namespace OnlineCasino.Repository.IRepository;

public interface IWalletRepository
{
	void CreateWallet(string userId, int currency);
	Task<Balance> GetWalletBalance(string userId);
}
