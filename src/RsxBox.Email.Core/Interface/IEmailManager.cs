using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsxBox.Email.Core.Interface
{
    public interface IEmailManager<TEmailTemplate, TPKType> : IEmailSender<TEmailTemplate>, IEmailTemplateManager<TEmailTemplate,TPKType>
    { }
}
