using Core.Profiles.NHibernate.Models;
using Core.Profiles.NHibernate.Static;
using Framework.Core.DomainModel;

namespace Core.Profiles.Models
{
    public class ProfileWidgetEditModel : IMappedModel<ProfileWidget, ProfileWidgetEditModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the display mode.
        /// </summary>
        /// <value>The display mode.</value>
        public ProfileWidgetDisplayMode DisplayMode { get; set; }

        #endregion

        public ProfileWidgetEditModel MapFrom(ProfileWidget from)
        {
            Id = from.Id;
            DisplayMode = from.DisplayMode;

            return this;
        }

        public ProfileWidget MapTo(ProfileWidget to)
        {
            to.Id = Id;
            to.DisplayMode = DisplayMode;

            return to;
        }
    }
}