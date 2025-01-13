using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineCasino.Models;

public class Balance
{
	public decimal CurrentBallance { get; set; }
	public string CurrencyName { get; set; }
}
