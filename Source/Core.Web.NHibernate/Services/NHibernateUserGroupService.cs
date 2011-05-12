using System;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;
using System.Linq;

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

        public IQueryable<UserGroup> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if(String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(userGroup => userGroup.Name.StartsWith(searchString));
        }
    }
}
