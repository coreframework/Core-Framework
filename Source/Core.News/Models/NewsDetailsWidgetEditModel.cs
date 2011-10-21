using Core.News.Nhibernate.Models;
using Framework.Core.DomainModel;

namespace Core.News.Models
{
    public class NewsDetailsWidgetEditModel : IMappedModel<NewsDetailsWidget, NewsDetailsWidgetEditModel>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [link by URL].
        /// </summary>
        /// <value><c>true</c> if [link by URL]; otherwise, <c>false</c>.</value>
        public bool LinkByUrl { get; set; }

        public NewsDetailsWidgetEditModel MapFrom(NewsDetailsWidget from)
        {
            Id = from.Id;
            LinkByUrl = from.LinkMode.Equals(NewsDetailsLinkMode.Url);

            return this;
        }

        public NewsDetailsWidget MapTo(NewsDetailsWidget to)
        {
            if (Id > 0)
                to.Id = Id;
            to.LinkMode = LinkByUrl ? NewsDetailsLinkMode.Url : NewsDetailsLinkMode.Id;
            
            return to;
        }
    }
}