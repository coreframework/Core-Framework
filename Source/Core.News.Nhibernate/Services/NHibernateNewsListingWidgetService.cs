using System.Collections.Generic;
using Castle.Facilities.NHibernateIntegration;
using Core.News.Nhibernate.Contracts;
using Core.News.Nhibernate.Models;
using Framework.Facilities.NHibernate;
using NHibernate;
using System.Linq;

namespace Core.News.Nhibernate.Services
{
    public class NHibernateNewsListingWidgetService : NHibernateDataService<NewsListingWidget>, INewsListingWidgetService
    {
        #region Constructors

        public NHibernateNewsListingWidgetService(ISessionManager sessionManager) : base(sessionManager)
        {

        }

        #endregion

        #region Methods

        #endregion
    }
}
