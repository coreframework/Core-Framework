using System.Linq;
using Core.Forms.NHibernate.Models;
using Framework.Core.Services;

namespace Core.Forms.NHibernate.Contracts
{
    public interface IFormWidgetAnswerService : IDataService<FormWidgetAnswer>
    {
        IQueryable<FormWidgetAnswer> GetAnswersQuery(long formWidgetId, string searchString);
    }
}
