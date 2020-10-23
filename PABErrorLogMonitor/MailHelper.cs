using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PABErrorLogMonitor
{
    public class MailHelper
    {
        private SmtpClient mail = new SmtpClient();
        MailMessage message = new MailMessage();
        public MailHelper()
        {
            mail.Port = int.Parse(ConfigurationManager.AppSettings["PORT"]);
            mail.Host = ConfigurationManager.AppSettings["HOST"];
            mail.UseDefaultCredentials = true;
            message.From = new MailAddress(ConfigurationManager.AppSettings["FROM"]);
            foreach (var address in ConfigurationManager.AppSettings["TO"].Split(new[] { ';' }))
            {
                if (!string.IsNullOrEmpty(address))
                    message.To.Add(address);
            }
            foreach (var address in ConfigurationManager.AppSettings["CC"].Split(new[] { ';' }))
            {
                if (!string.IsNullOrEmpty(address))
                    message.CC.Add(address);
            }
        }

        public bool Send(string subject, string body, MailPriority mailPriority = MailPriority.Normal)
        {
            message.Subject = subject;
            message.Body = body;
            message.Priority = mailPriority;
            message.IsBodyHtml = true;
            try
            {
                mail.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return true;
        }
    }
}
