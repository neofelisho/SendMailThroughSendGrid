using System;
using System.Net.Mail;
using System.Net.Mime;

namespace SendNetMailThroughSendGrid
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var mailMsg = new MailMessage();

                // To
                mailMsg.To.Add(new MailAddress("neofelisho@gmail.com", "Neo Ho"));

                // From
                mailMsg.From = new MailAddress("neofelisho@gmail.com", "Neo Ho");

                // Subject and multipart/alternative Body
                mailMsg.Subject = "Message from Neo Ho through SendGrid";
                const string text = "Hello World plain text!";
                const string html = @"<p>Hello World!</p>";
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                var smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                var credentials = new System.Net.NetworkCredential("azure_user_name@azure.com", "sendgrid_password");
                smtpClient.Credentials = credentials;

                smtpClient.Send(mailMsg);
                Console.WriteLine("Success.");
                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
