using RsxBox.Email.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsxBox.Email.Core.Interface
{
    public interface IEmailSender<TEmailTemplate>
    {
        void Send(TEmailTemplate template, StringTokens tokens, List<EmailAddress> addresses);
        void SendAsync(TEmailTemplate template, StringTokens tokens, List<EmailAddress> addresses);

    }
}
