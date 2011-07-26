using System;
using Core.Forms.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormLocaleService : IDataService<FormLocale>
    {
        FormLocale GetLocale(long formId, String culture);
    }
}