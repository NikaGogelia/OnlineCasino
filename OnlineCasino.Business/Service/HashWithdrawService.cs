using Azure.Core;
using System.Security.Cryptography;
using System.Text;

namespace OnlineCasino.Service;

public static class HashWithdrawService
{
	public static string GenerateHash(decimal amount, string merchantId, int transactionId, int usersAccountNumber, string usersFullName, string secretKey)
	{
		string input = $"{amount}{merchantId}{transactionId}{usersAccountNumber}{usersFullName}{secretKey}";

		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}
	}
}
