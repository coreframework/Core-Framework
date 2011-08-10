﻿using System;
using Framework.Core.Services;
using Core.News.Nhibernate.Models;

namespace Core.News.NHibernate.Contracts
{
    public interface INewsCategoryLocaleService : IDataService<NewsCategoryLocale>
    {
        /// <summary>
        /// Get Category Locale
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="culture">The culture</param>
        /// <returns>Category Locale</returns>
        NewsCategoryLocale GetLocale(long categoryId, String culture);
    }
}