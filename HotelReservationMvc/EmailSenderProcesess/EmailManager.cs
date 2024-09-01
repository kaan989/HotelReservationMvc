using System.Net.Mail;
using System.Text;

namespace HotelReservationMvc.EmailSenderProcesess
{
    public class EmailManager : IEmailManager
    {
        private readonly IConfiguration _configuration;

        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(EmailMessageModel model)
        {
            try
            {
                MailMessage mail = new MailMessage();
                // Set the sender address
                mail.From = new MailAddress("edutechkaan@outlook.com");
                // Add recipient address
                mail.To.Add(new MailAddress(model.To));
                // Set the subject and its encoding
                mail.Subject = model.Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                // Set the body of the email and specify that it's HTML
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Body = model.Body;
                // Note: Add CC recipients here if needed
                // Note: Add Bcc recipients here if needed

                SmtpClient client = new SmtpClient();
                // Configure the SMTP client
                client.Credentials = new System.Net.NetworkCredential("edutechkaan@outlook.com", "Kaan_2872");
                client.Port = 587; // Port 25 can also be used
                client.Host = "smtp-mail.outlook.com";
                client.EnableSsl = true; // Enable SSL encryption

                client.Send(mail); // Send the email
                return true;
            }
            catch (Exception)
            {
                return false; // Return false in case of error
            }
        }

        public bool SendEmailviaGmail(EmailMessageModel model)
        {
            try
            {
                // Retrieve Gmail settings from configuration
                var gmailaddress = _configuration.GetSection("GmailSettings:GmailAddress").Value;
                var token = _configuration.GetSection("GmailSettings:GmailToken").Value;
                var host = _configuration.GetSection("GmailSettings:GmailHost").Value;
                var port = Convert.ToInt32(_configuration.GetSection("GmailSettings:GmailPort").Value);
                var CC = _configuration.GetSection("CC").Value.Split(",");

                MailMessage mail = new MailMessage();
                // Set the sender address
                mail.From = new MailAddress(gmailaddress);
                // Add recipient address
                mail.To.Add(new MailAddress(model.To));
                // Set the subject and its encoding
                mail.Subject = model.Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                // Set the body of the email and specify that it's HTML
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Body = model.Body;
                // Add CC recipients if any
                if (CC != null && CC.Length > 0)
                {
                    foreach (var item in CC)
                    {
                        mail.CC.Add(new MailAddress(item));
                    }
                }

                SmtpClient client = new SmtpClient(host, port);
                // Configure the SMTP client with Gmail credentials
                client.Credentials = new System.Net.NetworkCredential(gmailaddress, token);
                client.EnableSsl = true; // Enable SSL encryption
                client.Send(mail); // Send the email
                return true;
            }
            catch (Exception ex)
            {
                return false; // Return false in case of error
            }
        }

        public async Task SendMailAsync(EmailMessageModel model)
        {
            try
            {
                MailMessage mail = new MailMessage();
                // Set the sender address
                mail.From = new MailAddress("hgyazilimsinifi@outlook.com");
                // Add recipient address
                mail.To.Add(new MailAddress(model.To));
                // Set the subject and its encoding
                mail.Subject = model.Subject;
                mail.SubjectEncoding = Encoding.UTF8;
                // Set the body of the email and specify that it's HTML
                mail.IsBodyHtml = true;
                mail.BodyEncoding = Encoding.UTF8;
                mail.Body = model.Body;
                // Note: Add CC recipients here if needed
                // Note: Add Bcc recipients here if needed

                SmtpClient client = new SmtpClient();
                // Configure the SMTP client
                client.Credentials = new System.Net.NetworkCredential("hgyazilimsinifi@outlook.com", "betulkadikoy2023");
                client.Port = 587; // Port 25 can also be used
                client.Host = "smtp-mail.outlook.com";
                client.EnableSsl = true; // Enable SSL encryption

                // Send the email asynchronously
                await client.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                // Log the exception details if needed
            }
        }
    }
}
