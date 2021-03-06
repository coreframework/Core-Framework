﻿using System;
using System.Linq;
using Core.Languages.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Languages.NHibernate.Contracts
{
    public interface ILanguageService : IDataService<Language>
    {
        /// <summary>
        /// Gets the default language.
        /// </summary>
        /// <value>The default language.</value>
        Language DefaultLanguage { get; }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <param name="baseQuery">The base query.</param>
        /// <returns></returns>
        int GetCount(IQueryable<Language> baseQuery);

        /// <summary>
        /// Gets the search query.
        /// </summary>
        /// <param name="searchString">The search string.</param>
        /// <returns></returns>
        IQueryable<Language> GetSearchQuery(String searchString);      
    }
}
