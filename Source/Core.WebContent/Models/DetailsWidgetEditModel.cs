using Core.WebContent.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.WebContent.Models
{
    public class DetailsWidgetEditModel : IMappedModel<WebContentDetailsWidget, DetailsWidgetEditModel>
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

        public DetailsWidgetEditModel MapFrom(WebContentDetailsWidget from)
        {
            Id = from.Id;
            LinkByUrl = from.LinkMode.Equals(WebContentDetailsLinkMode.Url);

            return this;
        }

        public WebContentDetailsWidget MapTo(WebContentDetailsWidget to)
        {
            if (Id > 0)
                to.Id = Id;
            to.LinkMode = LinkByUrl ? WebContentDetailsLinkMode.Url : WebContentDetailsLinkMode.Id;

            return to;
        }
    }
}