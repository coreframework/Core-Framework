using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Languages.NHibernate.Contracts;
using Core.Languages.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Languages.NHibernate.Services
{
    public class NHibernateLanguageService : NHibernateDataService<Language>, ILanguageService
    {
        public NHibernateLanguageService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public int GetCount(IQueryable<Language> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<Language> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(language => language.Title.Contains(searchString));
        }
    }
}
