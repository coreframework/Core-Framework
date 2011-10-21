using Castle.Facilities.NHibernateIntegration;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;
using System.Linq;

namespace Core.News.Nhibernate.Services
{
    public class NHibernateNewsDetailsWidgetService : NHibernateDataService<NewsDetailsWidget>, INewsDetailsWidgetService
    {
        #region Fields

        private NewsDetailsLinkMode? linkMode;

        #endregion

        #region Properties

        public NewsDetailsLinkMode LinkMode
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

        public NHibernateNewsDetailsWidgetService(ISessionManager sessionManager)
            : base(sessionManager)
        {

        }

        #endregion



        private NewsDetailsLinkMode GetLinkMode()
        {
            NewsDetailsWidget detailsWidget = GetAll().FirstOrDefault();
            return detailsWidget.LinkMode;
        }

        public override bool Save(NewsDetailsWidget entity)
        {
            linkMode = entity.LinkMode;

            return base.Save(entity);
        }
    }
}
