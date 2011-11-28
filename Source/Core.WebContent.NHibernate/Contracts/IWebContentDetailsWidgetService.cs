using Core.WebContent.NHibernate.Mappings;
using Core.WebContent.NHibernate.Models;
using Framework.Core.Services;

namespace Core.WebContent.NHibernate.Contracts
{
    public interface IWebContentDetailsWidgetService : IDataService<WebContentDetailsWidget>
    {
        WebContentDetailsLinkMode LinkMode { get; }
    }
}
