using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Web.NHibernate.Services
{
    public class NHibernatePageLayoutTemplateService : NHibernateDataService<PageLayoutTemplate>, IPageLayoutTemplateService
    {
        #region Constructors

        public NHibernatePageLayoutTemplateService(ISessionManager sessionManager) : base(sessionManager)
        {}

        #endregion

        #region Methods

        public PageLayoutTemplate FindDefault()
        {
            var query = CreateQuery().OrderBy(pageTemplate => pageTemplate.Priority);

            return query.FirstOrDefault();
        }

        #endregion

    }
}
