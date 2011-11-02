using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageTemplateService : NHibernateDataService<PageTemplate>, IPageTemplateService
    {
        #region Constructors

        public NHibernatePageTemplateService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public override IQueryable<PageTemplate> CreateQuery()
        {
            var baseQuery = base.CreateQuery();
            return baseQuery.Where(page => page.IsTemplate);
        }

        #endregion

    }
}
