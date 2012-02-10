using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateWebContentDetailsWidgetService : NHibernateDataService<WebContentDetailsWidget>, IWebContentDetailsWidgetService
    {
        #region Fields

        private WebContentDetailsLinkMode? linkMode;

        #endregion

        #region Properties

        public WebContentDetailsLinkMode LinkMode
        {
            get
            {
                if (!linkMode.HasValue)
                {
                    linkMode = GetLinkMode();
                }
                return linkMode.Value;
            }
        }

        #endregion

        #region Constructors

        public NHibernateWebContentDetailsWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {

        }

        #endregion

        private WebContentDetailsLinkMode GetLinkMode()
        {
            WebContentDetailsWidget detailsWidget = GetAll().FirstOrDefault();

            return detailsWidget.LinkMode;
        }

        public override bool Save(WebContentDetailsWidget entity)
        {
            linkMode = entity.LinkMode;

            return base.Save(entity);
        }
    }
}
