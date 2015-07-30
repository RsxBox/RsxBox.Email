using RsxBox.Email.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RsxBox.Email.Core.Interface;
using RsxBox.Email.Core.Models;

namespace RsxBox.Email.Core.InMemory
{
    

    public class InMemoryEmailTemplateManager<TEmailTemplate> : IEmailTemplateManager<TEmailTemplate, int>
        where TEmailTemplate : EmailTemplate
    {
        private List<TEmailTemplate> _emailTemplate;
        private Expression<Func<TEmailTemplate, int>> pkSelector = null;
        private Expression<Action<TEmailTemplate, int>> pkSetter = null;

        //public InMemoryEmailTemplateManager(List<TEmailTemplate> initEmailTemplates) : this()
        //{
        //    _emailTemplate.AddRange(initEmailTemplates);
        //}
        public InMemoryEmailTemplateManager()
        {
            _emailTemplate = new List<TEmailTemplate>();
            pkSelector = ExpressionUtility.GetModelPrimaryKeySelectorExpression<TEmailTemplate, int, KeyAttribute>();
            pkSetter = ExpressionUtility.GetModelPrimaryKeySetterExpression<TEmailTemplate, int, KeyAttribute>(pkSelector);

        }

        

        public void DeleteTemplate(int emailTemplatePk)
        {
            TEmailTemplate selected = SelectItemWithPK(emailTemplatePk);
            _emailTemplate.Remove(selected);

        }

        private TEmailTemplate SelectItemWithPK(int emailTemplatePk)
        {
            Expression<Func<TEmailTemplate, bool>> predicate = ExpressionUtility.CreatePredicateFrom<TEmailTemplate, int>(pkSelector, emailTemplatePk);

            IQueryable<TEmailTemplate> query = _emailTemplate.AsQueryable<TEmailTemplate>();
            var selected = query.Where(predicate).Single();
            return selected;
        }

        

        public IEnumerable<TEmailTemplate> GetAllTemplates(int offset, int size)
        {
            return _emailTemplate.Skip(offset).Take(size);
        }

        public TEmailTemplate GetTemplate(int emailTemplatePk)
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

        public TEmailTemplate CreateTemplate(TEmailTemplate template)
        {
            var maxPk = _emailTemplate.Max(t => pkSelector.Compile().Invoke(t));
            pkSetter.Compile().Invoke(template, maxPk + 1);
            _emailTemplate.Add(template);
            return template;
        }
    }
}
