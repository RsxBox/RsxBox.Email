using Microsoft.Data.Entity;
using RsxBox.Email.Core;
using RsxBox.Email.Core.Interface;
using RsxBox.Email.Core.Models;
using RsxBox.Email.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RsxBox.Email.EF.Managers
{
    public class EmailTemplateManager<TTemplateRepository> : EmailTemplateManager<EmailTemplate, TTemplateRepository>
        where TTemplateRepository : DbContext
    {
        public EmailTemplateManager(TTemplateRepository repository) : base(repository) { }
    }

    public class EmailTemplateManager<TEmailTemplate,TTemplateRepository> : IEmailTemplateManager<TEmailTemplate, int>
        where TTemplateRepository : DbContext
        where TEmailTemplate : class
    {
        protected TTemplateRepository repository;
        protected DbSet<TEmailTemplate> dbSet;
        protected Expression<Func<TEmailTemplate, int>> pkSelector;
        public EmailTemplateManager(TTemplateRepository repository)
        {
            this.repository = repository;
            this.dbSet = repository.Set<TEmailTemplate>();
            pkSelector = ExpressionUtility.GetModelPrimaryKeySelectorExpression<TEmailTemplate, int, KeyAttribute>();
        }

        public void DeleteTemplate(int emailTemplatePk)
        {
            var entity = GetTemplate(emailTemplatePk);
            dbSet.Remove(entity);
            repository.SaveChanges();
        }

        public IEnumerable<TEmailTemplate> GetAllTemplates(int offset, int size)
        {
            return dbSet;
        }

        public TEmailTemplate GetTemplate(int emailTemplatePk)
        {
            var predicate = ExpressionUtility.CreatePredicateFrom<TEmailTemplate, int>(pkSelector, emailTemplatePk);
            IQueryable<TEmailTemplate> query = dbSet;
            query = query.Where(predicate);
            var entity = query.Single();
            return entity;
        }

        public TEmailTemplate UpdateTemplate(TEmailTemplate modifiedTemplate)
        {
            var entity = GetTemplate(pkSelector.Compile().Invoke(modifiedTemplate));

            throw new NotImplementedException();
        }
    }
}
