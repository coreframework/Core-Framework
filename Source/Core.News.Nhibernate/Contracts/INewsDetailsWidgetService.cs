using Core.News.Nhibernate.Models;
using Framework.Core.Services;

namespace Core.News.Nhibernate.Contracts
{
    public interface INewsDetailsWidgetService : IDataService<NewsDetailsWidget>
    {
        NewsDetailsLinkMode LinkMode { get; }
    }
}
