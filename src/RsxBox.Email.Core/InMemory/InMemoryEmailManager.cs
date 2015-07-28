using RsxBox.Email.Core.Interface;
using RsxBox.Email.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsxBox.Email.Core.InMemory
{
    public class InMemoryEmailManager : IEmailManager<EmailTemplate, int>
    {
        private IEmailTemplateManager<EmailTemplate, int> templateManager;
        private IEmailSender<EmailTemplate> sender;

        public InMemoryEmailManager(IEmailTemplateManager<EmailTemplate, int> templateManager, IEmailSender<EmailTemplate> sender)
        {
            this.templateManager = templateManager;
            this.sender = sender;
        }
        public void DeleteTemplate(int emailTemplatePk)
        {
            templateManager.DeleteTemplate(emailTemplatePk);
        }

        public IEnumerable<EmailTemplate> GetAllTemplates(int offset, int size)
        {
            return templateManager.GetAllTemplates(offset, size);
        }

        public EmailTemplate GetTemplate(int emailTemplatePk)
        {
            return GetTemplate(emailTemplatePk);
        }

        public void Send(EmailTemplate template, StringTokens tokens, List<EmailAddress> addresses)
        {
            sender.Send(template, tokens, addresses);
        }

        public void SendAsync(EmailTemplate template, StringTokens tokens, List<EmailAddress> addresses)
        {
            sender.SendAsync(template, tokens, addresses);
        }

        public EmailTemplate UpdateTemplate(EmailTemplate modifiedTemplate)
        {
            return templateManager.UpdateTemplate(modifiedTemplate);
        }
    }
}
