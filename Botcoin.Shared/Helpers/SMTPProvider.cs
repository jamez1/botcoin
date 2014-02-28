using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Botcoin.Shared.Interfaces;

namespace Botcoin.Shared.Helpers
{
    public class SMTPProvider : ISMTPProvider
    {
        readonly IConfigurationManager config;
        SmtpClient client;

        public SMTPProvider(IConfigurationManager _config)
        {
            config = _config;

            client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential(config.SMTPUserName, config.SMTPPassword);
            client.Port = config.SMTPPort;
            client.Host = config.SMTPServer;
            client.EnableSsl = true;
        }

        public bool Send(string subject, string body)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //May be better to accept this from the caller and have the caller dictate who recieves
            //For the time being for simplicity there is a list of recipients for everything
            mail.To.Add(config.NoficationListeners);

            mail.From = new MailAddress(config.SMTPUserName, config.SMTPFrom, System.Text.Encoding.UTF8);

            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            mail.Body = body;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;

            //Maybe not
            //mail.Priority = MailPriority.High;


            try
            {
                client.Send(mail);
            }
            catch (Exception e)
            {
                e.ToString();
                return false;
            }
            return true;
        }
    }
}
