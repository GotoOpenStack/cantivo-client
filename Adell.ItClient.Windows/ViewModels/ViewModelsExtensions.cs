using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Adell.ItClient.Windows.ViewModels
{
    public static class ViewModelExtensions
    {
        public static void RaisePropertyChanged<T, TProperty>(this T baseViewModel, Expression<Func<T, TProperty>> expression) where T : BaseViewModel
        {
            baseViewModel.RaisePropertyChanged(baseViewModel.GetPropertyName(expression));
        }

        public static string GetPropertyName<T, TProperty>(this T owner, Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                    if (memberExpression == null)
                        throw new NotImplementedException();
                }
                else
                    throw new NotImplementedException();
            }

            var propertyName = memberExpression.Member.Name;
            return propertyName;
        }

    }
}
