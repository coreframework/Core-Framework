using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Forms.NHibernate.Services
{
    public class NHibernateFormWidgetAnswerService: NHibernateDataService<FormWidgetAnswer>, IFormWidgetAnswerService
    {
        public NHibernateFormWidgetAnswerService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Helper Methods

        public IQueryable<FormWidgetAnswer> GetAnswersQuery(long formWidgetId, String searchString)
        {
            var baseQuery = CreateQuery();
            return baseQuery.Where(answer => answer.FormBuilderWidget.Id == formWidgetId);
        }

        #endregion
    }
}
