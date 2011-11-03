using System;
using System.Linq;
using Castle.Facilities.NHibernateIntegration;
using Core.WebContent.NHibernate.Contracts;
using Core.WebContent.NHibernate.Models;
using Framework.Facilities.NHibernate;

namespace Core.WebContent.NHibernate.Services
{
    public class NHibernateSectionService: NHibernateDataService<Section>, ISectionService
    {
        public NHibernateSectionService(ISessionManager sessionManager) : base(sessionManager)
        {
          
        }

        public int GetCount(IQueryable<Section> baseQuery)
        {
            return baseQuery.Count();
        }
    }
}
