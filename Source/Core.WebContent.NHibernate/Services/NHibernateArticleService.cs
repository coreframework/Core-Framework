using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.Permissions.Contracts;
using Core.Framework.Permissions.Models;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Core.WebContent.NHibernate.Static;
using Framework.Facilities.NHibernate;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Criterion;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateArticleService: NHibernateDataService<Article>, IArticleService
    {
        public NHibernateArticleService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public IEnumerable<Article> GetPublishedArticles(ICorePrincipal user, int operation, ICollection categories)
        {
            var criteria = GetAllowedArticlesCriteria(user, operation);
            criteria.CreateAlias("Category", "category").Add(Restrictions.Eq("category.Status", CategoryStatus.Published)).Add(
                Restrictions.In("category.Id", categories));
            criteria.Add(Restrictions.Eq("Status", ArticleStatus.Published));
            return criteria.SetCacheable(true).List<Article>();
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
    }
}
