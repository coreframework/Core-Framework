﻿// --------------------------------------------------------------------------------------------------------------------
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
        /// Filter param name constant for query.
        /// </summary>
        public const string FilterParamNameForQuery = ":cultureCode";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CultureFilter"/> class.
        /// </summary>
        public CultureFilter()
        {
            WithName("CultureFilter").AddParameter(FilterParamName, NHibernateUtil.String);
        }

        #endregion
    }
}