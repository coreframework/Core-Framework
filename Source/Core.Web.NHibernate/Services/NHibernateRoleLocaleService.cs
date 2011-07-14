using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernateRoleLocaleService : NHibernateDataService<RoleLocale>, IRoleLocaleService
    {
        public NHibernateRoleLocaleService(ISessionManager sessionManager) : base(sessionManager)
        {

        }

        public RoleLocale GetLocale(long roleId, String culture)
        {
            IQueryable<RoleLocale> query = CreateQuery();
            return query.Where(locale => locale.Role.Id == roleId && locale.Culture == culture).FirstOrDefault();
        }
    }
}