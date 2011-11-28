using Core.WebContent.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.WebContent.NHibernate.Mappings
{
    public class WebContentDetailsWidgetMapping :  ClassMap<WebContentDetailsWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebContentDetailsWidgetMapping"/> class.
        /// </summary>
        public WebContentDetailsWidgetMapping()
        {
            Cache.Region("WebContent_DetailsWidgets").ReadWrite();
            Table("WebContent_DetailsWidgets");
            Id(detailsWidget => detailsWidget.Id);
            Map(detailsWidget => detailsWidget.LinkMode).CustomType(typeof(WebContentDetailsLinkMode));
        }
    }
}