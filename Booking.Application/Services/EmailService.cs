using Booking.Application.Services.ServiceDto;
using Booking.Domain.Interfaces.Services;
using MimeKit;

namespace Booking.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
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
 
        public async Task SendConfirmationEmailAsync(string email, string confirmCode)
        {
            string subject = "Confirm your email";
            string message = "<p>Hello,</p> <p>Just one more step before you get started.</p>" +
                $"<p>You must confirm your identity using the one-time pass code : <strong style=\"color:blue;\">{confirmCode}</strong></p>" +
                "<p>Note : This code will expire in 5 minutes.</p><div>Sincerely,</div> <div>BookingCl Team.</div>";

            await SendEmail(email, subject, message);
        }

        public async Task SendUpdatedConfirmationEmailAsync(string email, string confirmCode)
        {
            string subject = "Confirm your email";
            string message = "<p>Hello," +
                $"<p>Please confirm your identity using the one-time pass code : <strong style=\"color:blue;\">{confirmCode}</strong></p>" +
                "<p>Note : This code will expire in 5 minutes.</p><div>Sincerely,</div> <div>BookingCl Team.</div>";

            await SendEmail(email, subject, message);
        }

        public async Task SendConfirmationBookingEmailAsync(string email, string bookingCode, ConfirmationEmailData data )
        {
            string subject = "Booking confirmation";
            string message = getConfirmationMessage(data);
            //string message = "<p>Hello," +
            //    $"<p>Your booking is confirmed : <strong style=\"color:blue;\">{bookingCode}</strong></p>";

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

        private string getConfirmationMessage(ConfirmationEmailData data)
        {
            return "<!DOCTYPE html>" +
                "<html lang=\"en\">" +
                "<head>" +
                "<meta charset=\"UTF-8\">" +
                "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
                "<title>Document</title>" +
                "<style>" +
                "div {" +
                "text-align: left;" +
                " font-size: 16px;" +
                "font-family:Arial, Helvetica, sans-serif;" +
                "}" +
                ".main_body{" +
                "width: 80vw;" +
                "margin: auto;" +
                "}" +
                ".block_div{" +
                "display: inline-block;" +
                "}" +
                "</style>" +
                "</head>" +
                "<div class=\"main_body\">" +
                "<h2 style=\"color: #581ADB; text-align: left;\">Hotel for you.</h2>" +
                "<h2 style=\"color: #581ADB; text-align: center;\">Booking Details</h2>" +
                "<div class=\"block_div\" style=\"float: left; width: calc(100% - 51%);\">" +
                $"<div>Address: {data.Address}, {data.City}</div>" +
                $"<div>Booking code confirmation: {data.Code}</div>" +
                $"<div style=\"text-align: left; margin-bottom: 15px;\">Email address: {data.Email}</div>" +
                $"<div>Check-in: {data.CheckIn.ToString("MMMM dd, yyyy")}</div>" +
                $"<div>Check-out: {data.CheckIn.ToString("MMMM dd, yyyy")} December 27, 2024</div>" +
                $"<div style=\"margin-top: 5px; margin-bottom: 5px; color: #581ADB;\">Date until change: {data.ChangeDate.ToString("MMMM dd, yyyy")}</div>" +
                $"<div>Room quantity: {data.RoomQuantity}</div>" +
                $"<div>Room price per night: {data.RoomPrice}</div>" +
                $"<div>Expected number of people: {data.Adults} adults, {data.Children} children</div>" +
                "<div style=\"margin-top: 30px; margin-bottom: 30px; color: #581ADB;\">Urban Central is waiting for you</div></div>" +
                "<div class=\"block_div\" style=\"float: right; width: 40%\">" +
                $"<img style=\"width:100%; margin:auto\" id=\"hotelImage\" alt=\"Hotel Azure\" src=\"{data.Image}\"></div>" +
                "</div>" +
                "</html>";
        } 
    }
}
