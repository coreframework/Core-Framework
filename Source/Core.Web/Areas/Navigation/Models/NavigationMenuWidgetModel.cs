using Core.Web.NHibernate.Models.Static;
using Core.Web.NHibernate.Models.Widgets;
using Framework.Core.DomainModel;

namespace Core.Web.Areas.Navigation.Models
{
    public class NavigationMenuWidgetModel : IMappedModel<NavigationMenuWidget, NavigationMenuWidgetModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public Orientation Orientation { get; set; }

        #endregion

        public NavigationMenuWidgetModel MapFrom(NavigationMenuWidget from)
        {
            Id = from.Id;
            Orientation = from.Orientation;
            return this;
        }

        public NavigationMenuWidget MapTo(NavigationMenuWidget to)
        {
            to.Id = Id;
            to.Orientation = Orientation;
            return to;
        }
    }
}