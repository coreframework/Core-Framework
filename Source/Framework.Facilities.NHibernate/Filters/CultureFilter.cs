// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CultureFilter.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using FluentNHibernate.Mapping;
using Framework.Core.Localization;
using NHibernate;

namespace Framework.Facilities.NHibernate.Filters
{
    /// <summary>
    /// NHibernate filter for culture.
    /// </summary>
    public class CultureFilter : FilterDefinition
    {
        #region Constants

        /// <summary>
        /// Filter param name constant.
        /// </summary>
        public static readonly String FilterParamName = "cultureCode";

        /// <summary>
        /// Default culture filter param name constant.
        /// </summary>
        public static readonly String DefaultCultureFilterParamName = "defaultCultureCode";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureFilter"/> class.
        /// </summary>
        public CultureFilter()
        {
            WithName("CultureFilter").WithCondition(String.Format("Culture = :{0} OR Culture = :{1} OR Culture IS NULL", FilterParamName, DefaultCultureFilterParamName))
                .AddParameter(FilterParamName, NHibernateUtil.String).AddParameter(DefaultCultureFilterParamName, NHibernateUtil.String);
        }

        /// <summary>
        /// Returns the culture priority filter expression.
        /// </summary>
        /// <returns>Returns the culture priority filter.</returns>
        public static String CultureFilterPriorityExpression()
        {
            return String.Format("case when Culture = '{0}' then 3 when Culture='{1}' then 2 when Culture IS NULL then 1 else 0 end", Thread.CurrentThread.CurrentCulture.Name, CultureHelper.DefaultCultureName);
        }

        #endregion
    }
}
