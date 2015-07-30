#if DNX451

using RsxBox.Email.Core.Interface;
using RsxBox.Email.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RsxBox.Email.Core.InMemory
{
    public class InMemoryEmailSender<TEmailTemplate> : IEmailSender<TEmailTemplate>
        where TEmailTemplate : EmailTemplate
    {
        SmtpClient client;
        public InMemoryEmailSender(SmtpClient client)
        {
            this.client = client;
        }
        public void Send(TEmailTemplate template, StringTokens tokens, List<EmailAddress> addresses)
        {
            StringBuilder body = ReplaceTokens(template.HtmlTemplate, tokens);
            StringBuilder subject = ReplaceTokens(template.SubjectTemplate, tokens);
            MailMessage email = FormEmail(addresses, body, subject);

            client.Send(email);
        }

        public void SendAsync(TEmailTemplate template, StringTokens tokens, List<EmailAddress> addresses)
        {
            StringBuilder body = ReplaceTokens(template.HtmlTemplate, tokens);
            StringBuilder subject = ReplaceTokens(template.SubjectTemplate, tokens);
            MailMessage email = FormEmail(addresses, body, subject);

            client.SendMailAsync(email);
        }

        private static MailMessage FormEmail(List<EmailAddress> addresses, StringBuilder body, StringBuilder subject)
        {
            MailMessage email = new MailMessage();
            var toList = addresses.Where(t => t.Type == EmailAddresseeType.TO).ToList();
            var ccList = addresses.Where(t => t.Type == EmailAddresseeType.CC).ToList();
            var bccList = addresses.Where(t => t.Type == EmailAddresseeType.BCC).ToList();

            toList.ForEach(address => email.To.Add(new MailAddress(address.Email, address.AddresseeName, Encoding.UTF8)));
            ccList.ForEach(address => email.CC.Add(new MailAddress(address.Email, address.AddresseeName, Encoding.UTF8)));
            bccList.ForEach(address => email.Bcc.Add(new MailAddress(address.Email, address.AddresseeName, Encoding.UTF8)));

            email.Body = body.ToString();
            email.IsBodyHtml = true;

            email.Subject = subject.ToString();
            return email;
        }

        private static StringBuilder ReplaceTokens(string template, StringTokens tokens)
        {
            StringBuilder sb = new StringBuilder(template);
            foreach (var kv in tokens)
            {
                sb.Replace(kv.Key, kv.Value);
            }

            return sb;
        }

        
    }
}

#endif