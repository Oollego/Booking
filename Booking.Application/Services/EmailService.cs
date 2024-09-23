using Booking.Domain.Interfaces.Services;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Booking.Domain.Dto.User;
using Booking.Domain.Result;

namespace Booking.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer = null!;
        private readonly int _smtpPort;
        private readonly bool _useSsl;
        private readonly string _login;
        private readonly string _password;

        public EmailService(string smtpServer, int smtpPort, bool useSsl, string login, string password)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _useSsl = useSsl;
            _login = login;
            _password = password;
        }
        public async Task SendBookEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmCode)
        {
            string subject = "Confirm your email";
            string message = "<p>Hello,</p> <p>Just one more step before you get started.</p>" +
                $"<p>You must confirm your identity using the one-time pass code : <strong style=\"color:blue;\">{confirmCode}</strong></p>" +
                "<p>Note : This code will expire in 10 minutes.</p><div>Sincerely,</div> <div>BookingCl Team.</div>";

            await SendEmail(email, subject, message);
        }

        private async Task SendEmail(string email, string subject, string message) 
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Administration of BookingCl", _login));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new MailKit.Net.Smtp.SmtpClient();

            await client.ConnectAsync(_smtpServer, _smtpPort, _useSsl);
            await client.AuthenticateAsync(_login, _password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);

        }
    }
}
