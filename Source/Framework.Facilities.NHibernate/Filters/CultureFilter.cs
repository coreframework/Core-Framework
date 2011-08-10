// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CultureFilter.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using FluentNHibernate.Mapping;
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
        public const string FilterParamName = "cultureCode";

        /// <summary>
        /// Default culture filter param name constant.
        /// </summary>
        public const string DefaultCultureFilterParamName = "defaultCultureCode";

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

        #endregion
    }
}
