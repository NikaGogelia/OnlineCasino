using System.Security.Cryptography;
using System.Text;

namespace OnlineCasino.Service;

public static class HashDepositService
{
	public static string GenerateHash(decimal amount, string merchantId, int transactionId, string secretKey)
	{
		string input = $"{amount}{merchantId}{transactionId}{secretKey}";

		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
			return BitConverter.ToString(bytes).Replace("-", "").ToLower();
		}
	}
}
