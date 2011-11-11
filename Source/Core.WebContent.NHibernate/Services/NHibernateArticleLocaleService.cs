using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateArticleLocaleService : NHibernateDataService<ArticleLocale>, IArticleLocaleService
    {
        public NHibernateArticleLocaleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public ArticleLocale GetLocale(long articleId, String culture)
        {
            IQueryable<ArticleLocale> query = CreateQuery();
            return query.Where(locale => locale.Article.Id == articleId && locale.Culture == culture).FirstOrDefault();
        }

        public ICriteria GetSearchCriteria(String searchString, ICorePrincipal user, int operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<ArticleLocale>().CreateAlias("Article", "article");

            DetachedCriteria filter = DetachedCriteria.For<ArticleLocale>("filteredLocale").CreateAlias("Article", "filteredArticle")
                .SetProjection(Projections.Id()).SetMaxResults(1).AddOrder(Order.Desc("filteredLocale.Priority")).Add(
                    Restrictions.EqProperty("filteredArticle.Id", "article.Id"));

            criteria.Add(Subqueries.PropertyIn("Id", filter));

            //apply permissions criteria

            var articlesCriteria = DetachedCriteria.For<Article>("articles");
            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(Article),
                                                                                    "articles.Id", "articles.UserId");
            if (permissionCriteria != null)
            {
                articlesCriteria.Add(permissionCriteria).SetProjection(Projections.Id());
                criteria.Add(Subqueries.PropertyIn("article.Id", articlesCriteria));
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                criteria.Add(Restrictions.Like("Title", searchString, MatchMode.Anywhere));
            }

            return criteria.SetCacheable(true);
        }
    }
}
