using System.ComponentModel.DataAnnotations;

namespace RsxBox.Email.Core.Models
{
    public class EmailTemplate
    {
        [Key]
        public int PK { get; set; }
        public string SubjectTemplate { get; set; }
        public string HtmlTemplate { get; set; }
    }
}
