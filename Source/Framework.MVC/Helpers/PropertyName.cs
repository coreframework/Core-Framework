// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertyName.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace Framework.Mvc.Helpers
{
    /// <summary>
    /// Gets property name using lambda expressions.
    /// </summary>
    /// <remarks>
    /// Source http://www.lesnikowski.com/blog/index.php/property-name-from-lambda/.
    /// </remarks>
    public static class PropertyName
    {
        /// <summary>
        /// Gets property name by lambda expression.
        /// </summary>
        /// <typeparam name="T">Expression root type.</typeparam>
        /// <param name="expression">The lambda expression.</param>
        /// <returns>String property name.</returns>
        public static String For<T>(Expression<Func<T, Object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }

        /// <summary>
        /// Gets property name by lambda expression.
        /// </summary>
        /// <param name="expression">The lambda expression.</param>
        /// <returns>String property name.</returns>
        public static String For(Expression<Func<Object>> expression)
        {
            Expression body = expression.Body;
            return GetMemberName(body);
        }

        private static String GetMemberName(Expression expression)
        {
            if (expression is MemberExpression)
            {
                var memberExpression = (MemberExpression)expression;

             /*   if (memberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(memberExpression.Expression) + "." + memberExpression.Member.Name;
                }*/

                return memberExpression.Member.Name;
            }

            if (expression is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)expression;

                if (unaryExpression.NodeType != ExpressionType.Convert)
                {
                    throw new InvalidOperationException(String.Format("Cannot interpret member from {0}", expression));
                }

                return GetMemberName(unaryExpression.Operand);
            }

            throw new InvalidOperationException(String.Format("Could not determine member from {0}", expression));
        }
    }
}