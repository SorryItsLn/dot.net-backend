namespace BookStore.Core.Abstractions.Auth;

public interface IEmailSender
{
    Task SendAsync(string to, string subject, string htmlBody);
}
