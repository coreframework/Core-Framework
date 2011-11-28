using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Permissions;
using Core.WebContent.NHibernate.Static;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateArticleService : NHibernateDataService<Article>, IArticleService
    {
        public NHibernateArticleService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        public IEnumerable<Article> GetPublishedArticles(ICorePrincipal user, int operation, ICollection categories)
        {
            var criteria = GetPublishedArticlesCriteria(user, operation, categories);
            return criteria.SetCacheable(true).List<Article>();
        }

        public ICriteria GetArticlesCriteria(ICollection categories)
        {
            var criteria = Session.CreateCriteria<Article>();

            //filter by categories
            criteria.CreateAlias("Category", "category").Add(Restrictions.Eq("category.Status", CategoryStatus.Published)).Add(
             Restrictions.In("category.Id", categories));

            //filter by article status
            criteria.Add(Restrictions.Eq("Status", ArticleStatus.Published)).Add(
                Restrictions.Or(
                Restrictions.IsNull("StartPublishingDate"),
                Restrictions.Le("StartPublishingDate", DateTime.Now))).Add(
                Restrictions.Or(
                Restrictions.IsNull("FinishPublishingDate"),
                Restrictions.Ge("FinishPublishingDate", DateTime.Now)));
            return criteria;
        }

        private ICriteria GetAllowedArticlesCriteria(ICorePrincipal user, Int32 operationCode)
        {
            ICriteria criteria = Session.CreateCriteria<Article>("articles");

            var permissionCommonService = ServiceLocator.Current.GetInstance<IPermissionCommonService>();
            var permissionCriteria = permissionCommonService.GetPermissionsCriteria(user, operationCode, typeof(WebContentCategory),
                                                                                    "articles.Id", "articles.UserId");
            if (permissionCriteria != null)
                criteria.Add(permissionCriteria);
            return criteria;
        }

        public ICriteria GetPublishedArticlesCriteria(ICorePrincipal user, int operation, ICollection categories)
        {
            var criteria = GetAllowedArticlesCriteria(user, operation);
            if (categories != null)
            {
                criteria.CreateAlias("Category", "category").Add(Restrictions.Eq("category.Status",
                                                                                 CategoryStatus.Published)).Add(
                                                                                     Restrictions.In("category.Id",
                                                                                                     categories));
            }
            criteria.Add(Restrictions.Eq("Status", ArticleStatus.Published)).Add(
              Restrictions.Or(
              Restrictions.IsNull("StartPublishingDate"),
              Restrictions.Le("StartPublishingDate", DateTime.Now))).Add(
              Restrictions.Or(
              Restrictions.IsNull("FinishPublishingDate"),
              Restrictions.Ge("FinishPublishingDate", DateTime.Now)));

            return criteria;
        }

        public Article FindPublished(ICorePrincipal user, long id)
        {
            var criteria = GetPublishedArticlesCriteria(user, (Int32)ArticleOperations.View, null);
            criteria.Add(Restrictions.Eq("Id", id));

            return (Article)criteria.UniqueResult();
        }

        public Article FindPublished(ICorePrincipal user, String url)
        {
            var criteria = GetPublishedArticlesCriteria(user, (Int32)ArticleOperations.View, null);
            criteria.Add(Restrictions.Eq("Url", url));

            return (Article)criteria.UniqueResult();
        }
    }
}
