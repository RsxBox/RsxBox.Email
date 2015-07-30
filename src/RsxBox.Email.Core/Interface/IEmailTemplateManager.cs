using RsxBox.Email.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RsxBox.Email.Core.Interface
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public interface IEmailTemplateManager<TEmailTemplate, TPkType>
    {
        IEnumerable<TEmailTemplate> GetAllTemplates(int offset, int size);
        TEmailTemplate GetTemplate(TPkType emailTemplatePk);
        TEmailTemplate CreateTemplate(TEmailTemplate template);
        TEmailTemplate UpdateTemplate(TEmailTemplate modifiedTemplate);
        void DeleteTemplate(TPkType emailTemplatePk); 

    }
}
