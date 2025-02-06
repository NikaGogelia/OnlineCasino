using Microsoft.AspNetCore.Identity.UI.Services;

namespace OnlineCasino.Services;

public class EmailSender : IEmailSender
{
	public Task SendEmailAsync(string email, string subject, string htmlMessage)
	{
		return Task.CompletedTask;
	}
}
