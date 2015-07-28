using RsxBox.Email.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RsxBox.Email.Core.Interface;

namespace RsxBox.Email.Core.InMemory
{
    public class InMemoryEmailTemplateManager<TEmailTemplate> : InMemoryEmailTemplateManager<TEmailTemplate, int>, IEmailTemplateManager<TEmailTemplate, int>
    {
        public InMemoryEmailTemplateManager(List<TEmailTemplate> initEmailTemplates) : base(initEmailTemplates) { }
        public InMemoryEmailTemplateManager() : base() { }

    }

    public class InMemoryEmailTemplateManager<TEmailTemplate, TPkType> : IEmailTemplateManager<TEmailTemplate, TPkType>
    {
        private List<TEmailTemplate> _emailTemplate;
        private Expression<Func<TEmailTemplate, TPkType>> pkSelector = null;

        public InMemoryEmailTemplateManager(List<TEmailTemplate> initEmailTemplates) : this()
        {
            _emailTemplate.AddRange(initEmailTemplates);
        }
        public InMemoryEmailTemplateManager()
        {
            _emailTemplate = new List<TEmailTemplate>();
            pkSelector = ExpressionUtility.GetModelPrimaryKeySelectorExpression<TEmailTemplate, TPkType, KeyAttribute>();
        }

        

        public void DeleteTemplate(TPkType emailTemplatePk)
        {
            TEmailTemplate selected = SelectItemWithPK(emailTemplatePk);
            _emailTemplate.Remove(selected);

        }

        private TEmailTemplate SelectItemWithPK(TPkType emailTemplatePk)
        {
            Expression<Func<TEmailTemplate, bool>> predicate = ExpressionUtility.CreatePredicateFrom<TEmailTemplate, TPkType>(pkSelector, emailTemplatePk);

            IQueryable<TEmailTemplate> query = _emailTemplate.AsQueryable<TEmailTemplate>();
            var selected = query.Where(predicate).Single();
            return selected;
        }

        

        public IEnumerable<TEmailTemplate> GetAllTemplates(int offset, int size)
        {
            return _emailTemplate.Skip(offset).Take(size);
        }

        public TEmailTemplate GetTemplate(TPkType emailTemplatePk)
        {
            TEmailTemplate selected = SelectItemWithPK(emailTemplatePk);
            return selected;
        }

        public TEmailTemplate UpdateTemplate(TEmailTemplate modifiedTemplate)
        {
            var pk = pkSelector.Compile().Invoke(modifiedTemplate);
            TEmailTemplate selected = SelectItemWithPK(pk);
            var selectedIndex = _emailTemplate.IndexOf(selected);
            _emailTemplate[selectedIndex] = modifiedTemplate;
            return modifiedTemplate;
        }
    }
}
