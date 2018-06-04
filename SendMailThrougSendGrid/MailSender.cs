using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SendMailThrougSendGrid
{
    internal class MailSender
    {
        private static async Task Main()
        {
            var msg = new SendGridMessage();

            msg.SetFrom(new EmailAddress("neofelisho@gmail.com", "Neo Ho"));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress("neofelisho@gmail", "Neo Ho"),
                //new EmailAddress("anna@example.com", "Anna Lidman"),
                //new EmailAddress("peter@example.com", "Peter Saddow")
            };
            msg.AddTos(recipients);

            msg.SetSubject("Message from Neo Ho through SendGrid");

            msg.AddContent(MimeType.Text, "Hello World plain text!");
            msg.AddContent(MimeType.Html, "<p>Hello World!</p>");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, string.Format("..{0}..{0}..{0}", Path.DirectorySeparatorChar)))
                .AddJsonFile("appsettings.json");
            var apiKey = builder.Build().GetSection("SENDGRID_API_KEY").Value;

            var client = new SendGridClient(apiKey);
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine(response.StatusCode);
            Console.Read();
        }
    }
}
