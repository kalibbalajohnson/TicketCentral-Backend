using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace TicketCentral.Modules.Auth.Services;

public class EmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(
            "Ticket Central",
            "johnsonkalibbala@gmail.com"));

        email.To.Add(MailboxAddress.Parse(to));

        email.Subject = subject;

        email.Body = new TextPart("html")
        {
            Text = body
        };

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            "smtp.gmail.com",
            587,
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            "johnsonkalibbala@gmail.com", 
            "hpiu aqew gjoj lfgx");

        await smtp.SendAsync(email);

        await smtp.DisconnectAsync(true);
    }
}