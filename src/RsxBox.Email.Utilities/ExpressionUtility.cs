using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace RsxBox.Email.Utilities
{
    public static class ExpressionUtility
    {
        public static Expression<Func<TEmailTemplate, bool>> CreatePredicateFrom<TEmailTemplate, TPkType>(Expression<Func<TEmailTemplate, TPkType>> pkSelector, TPkType emailTemplatePk)
        {
            return Expression.Lambda<Func<TEmailTemplate, bool>>(
                                        Expression.Equal(pkSelector.Body, Expression.Constant(emailTemplatePk)), pkSelector.Parameters);
        }

        public static Expression<Func<TModel, TPkType>> GetModelPrimaryKeySelectorExpression<TModel, TPkType, TPrimaryKey>()
        {
            Type entityType = typeof(TModel);
            var propertyInfo = AttributeUtility.GetProperties(entityType, typeof(TPrimaryKey)).Single();
            var parameter = Expression.Parameter(entityType, "entity");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda<Func<TModel, TPkType>>(property, parameter);
            return lambda;
        }

        public static Expression<Action<TModel, TPkType>> GetModelPrimaryKeySetterExpression<TModel, TPkType, TPrimaryKey>()
        {
            var expression = GetModelPrimaryKeySelectorExpression<TModel, TPkType, TPrimaryKey>();
            var memberExpression = (MemberExpression)expression.Body;
            var property = (PropertyInfo)memberExpression.Member;
            var setMethod = property.GetSetMethod();

            var parameterT = Expression.Parameter(typeof(TModel), "x");
            var parameterTProperty = Expression.Parameter(typeof(TPkType), "y");

            var newExpression =
                Expression.Lambda<Action<TModel, TPkType>>(
                    Expression.Call(parameterT, setMethod, parameterTProperty),
                    parameterT,
                    parameterTProperty
                );

            return newExpression;
        }

        public static Expression<Action<TModel, TPkType>> GetModelPrimaryKeySetterExpression<TModel, TPkType, TPrimaryKey>
            (Expression<Func<TModel, TPkType>> selectorExpression)
        {
            var expression = selectorExpression;
            var memberExpression = (MemberExpression)expression.Body;
            var property = (PropertyInfo)memberExpression.Member;
            var setMethod = property.GetSetMethod();

            var parameterT = Expression.Parameter(typeof(TModel), "x");
            var parameterTProperty = Expression.Parameter(typeof(TPkType), "y");

            var newExpression =
                Expression.Lambda<Action<TModel, TPkType>>(
                    Expression.Call(parameterT, setMethod, parameterTProperty),
                    parameterT,
                    parameterTProperty
                );

            return newExpression;
        }
    }
}
