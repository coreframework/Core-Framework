using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateRoleService : NHibernateDataService<Role>, IRoleService
    {
        #region Constructors

        public NHibernateRoleService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        public int GetCount(IQueryable<Role> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<Role> GetSearchQuery(string searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery;
            }
            return baseQuery.Where(role => role.Name.Contains(searchString));
        }
    }
}
