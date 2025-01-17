﻿using OnlineCasino.Models;

namespace OnlineCasino.Repository.IRepository;

public interface IWalletRepository
{
	Task<Balance> GetWalletBalance(string userId);
	void CreateWallet(string userId, int currency);
}
