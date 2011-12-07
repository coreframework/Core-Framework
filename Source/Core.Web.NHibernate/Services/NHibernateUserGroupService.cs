using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Framework.NHibernate.Contracts;
using Core.Framework.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateUserGroupService : NHibernateDataService<UserGroup>, IUserGroupService
    {
        #region Constructors

        public NHibernateUserGroupService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        public int GetCount(IQueryable<UserGroup> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<UserGroup> GetSearchQuery(String searchString)
        {
            var baseQuery = CreateQuery();
            if(String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(userGroup => userGroup.Name.Contains(searchString));
        }
    }
}
