using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RsxBox.Email.Core.Models
{
    public class EmailTemplate
    {
        /*[Key]*/
        public int PK { get; set; }
        public string SubjectTemplate { get; set; }
        public string HtmlTemplate { get; set; }
    }
}
