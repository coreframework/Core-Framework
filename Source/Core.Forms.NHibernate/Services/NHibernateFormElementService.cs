using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.Forms.NHibernate.Services
{
    public class NHibernateFormElementService: NHibernateDataService<FormElement>, IFormElementService
    {
        public NHibernateFormElementService(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }


        public int GetCount(IQueryable<FormElement> baseQuery)
        {
            return baseQuery.Count();
        }

        public IQueryable<FormElement> GetSearchQuery(long formId,String searchString)
        {
            var baseQuery = CreateQuery();
            if (String.IsNullOrEmpty(searchString))
            {
                return baseQuery.Where(formElement => formElement.Form.Id == formId);
            }
            return baseQuery.Where(formElement => formElement.Form.Id == formId && formElement.Title.Contains(searchString));
        }

        public Int32 GetLastOrderNumber(long? formId)
        {
            var query = from formElement in CreateQuery()
                        where formElement.Form.Id == formId
                        select formElement;

            return query.Count() + 1;
        }
    }
}
