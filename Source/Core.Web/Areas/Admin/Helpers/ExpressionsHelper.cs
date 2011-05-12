// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExpressionsHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Core.Web.Areas.Admin.Helpers
{
    /// <summary>
    /// Extracts controllers and action data from lambda expressions.
    /// </summary>
    public static class ExpressionsHelper
    {
        private const String ControllerSuffix = "Controller";

        private const String AreasNamespace = "Areas";

        private const String AreaNameParam = "area";

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <typeparam name="T">Type of controller.</typeparam>
        /// <returns>Name of controller.</returns>
        public static String GetControllerName<T>()
            where T : IController
        {
            return typeof(T).Name.Replace(ControllerSuffix, String.Empty);
        }

        /// <summary>
        /// Gets the name of the area.
        /// </summary>
        /// <typeparam name="T">Type of controller.</typeparam>
        /// <returns>Name of area.</returns>
        public static String GetAreaName<T>()
            where T : IController
        {
            var chains = typeof(T).Namespace.Split('.').SkipWhile(x => !x.Equals(AreasNamespace)).Skip(1);
            return chains.FirstOrDefault() ?? String.Empty;
        }

        /// <summary>
        /// Gets the name of the action from lambda expression.
        /// </summary>
        /// <typeparam name="T">Controller type.</typeparam>
        /// <param name="action">The action expression.</param>
        /// <returns>Name of called action.</returns>
        public static String GetActionName<T>(Expression<Action<T>> action)
            where T : IController
        {
            var call = action.Body as MethodCallExpression;

            if (call == null)
            {
                throw new ArgumentException("Action expression should be a method call.");
            }

            if (call.Object != action.Parameters[0])
            {
                throw new InvalidOperationException("Method call must target lambda argument");
            }

            return call.Method.Name;
        }

        /// <summary>
        /// Gets route values from lambda expression.
        /// </summary>
        /// <typeparam name="T">Controller type.</typeparam>
        /// <param name="action">The action expression.</param>
        /// <returns>Route values.</returns>
        public static RouteValueDictionary GetRouteValues<T>(Expression<Action<T>> action)
            where T : IController
        {
            var call = action.Body as MethodCallExpression;

            if (call == null)
            {
                throw new ArgumentException("Action expression should be a method call.");
            }

            if (call.Object != action.Parameters[0])
            {
                throw new InvalidOperationException("Method call must target lambda argument");
            }

            ParameterInfo[] parameters = call.Method.GetParameters();
            var routeValues = new RouteValueDictionary();
            for (int i = 0; i < parameters.Length; i++)
            {
                Object value;
                Expression argument = call.Arguments[i];
                var constant = argument as ConstantExpression;
                if (constant != null)
                {
                    // If argument is a constant expression, just get the value.
                    value = constant.Value;
                }
                else
                {
                    // Otherwise, convert the argument subexpression to type object,
                    // make a lambda out of it, compile it, and invoke it to get the value.
                    var lambda = Expression.Lambda<Func<object>>(Expression.Convert(argument, typeof(object)));
                    try
                    {
                        value = lambda.Compile()();
                    }
                    catch
                    {
                        value = null;
                    }
                }

                if (value != null)
                {
                    routeValues[parameters[i].Name] = value;
                }
            }

            return routeValues;
        }

        /// <summary>
        /// Gets action url.
        /// </summary>
        /// <typeparam name="T">Type of controller.</typeparam>
        /// <param name="url">The URL helper instance that this method extends.</param>
        /// <param name="action">The action.</param>
        /// <returns>Action url.</returns>
        public static String Action<T>(this UrlHelper url, Expression<Action<T>> action)
            where T : IController
        {
            var areaName = GetAreaName<T>();
            var controllerName = GetControllerName<T>();
            var actionName = GetActionName(action);
            var routeValues = GetRouteValues(action);
            if (!String.IsNullOrEmpty(areaName))
            {
                routeValues[AreaNameParam] = areaName;
            }
            return url.Action(actionName, controllerName, routeValues);
        }

        /// <summary>
        /// Gets the parameter value from expression.
        /// </summary>
        /// <typeparam name="T">Type of controller.</typeparam>
        /// <param name="action">The action.</param>
        /// <param name="paramName">Name of the param.</param>
        /// <returns>Argument value for <paramref name="paramName"/> specified.</returns>
        public static Object GetParamValue<T>(Expression<Action<T>> action, String paramName)
        {
            var call = action.Body as MethodCallExpression;

            if (call == null)
            {
                throw new ArgumentException("Action expression should be a method call.");
            }

            if (call.Object != action.Parameters[0])
            {
                throw new InvalidOperationException("Method call must target lambda argument");
            }

            Object value;
            var parameter = call.Method.GetParameters().Where(p => p.Name.Equals(paramName)).FirstOrDefault();
            if (parameter == null)
            {
                throw new ArgumentException(String.Format("Action does not contain param named {0}.", paramName));
            }

            Expression argument = call.Arguments[parameter.Position];
            var constant = argument as ConstantExpression;
            if (constant != null)
            {
                // If argument is a constant expression, just get the value.
                value = constant.Value;
            }
            else
            {
                // Otherwise, convert the argument subexpression to type object,
                // make a lambda out of it, compile it, and invoke it to get the value.
                var lambda = Expression.Lambda<Func<object>>(Expression.Convert(argument, typeof(object)));
                try
                {
                    value = lambda.Compile()();
                }
                catch
                {
                    value = null;
                }
            }

            return value;
        }
    }
}