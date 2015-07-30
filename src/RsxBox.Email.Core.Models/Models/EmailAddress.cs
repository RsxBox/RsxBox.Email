using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsxBox.Email.Core.Models
{
    public class EmailAddress
    {
        public string AddresseeName { get; set; }
        public string Email { get; set; }
        public EmailAddresseeType Type { get; set; }
    }

    public enum EmailAddresseeType
    {
        TO,
        CC,
        BCC

    }
}
