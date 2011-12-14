using Core.Profiles.NHibernate.Static;
using FluentNHibernate.Data;

namespace Core.Profiles.NHibernate.Models
{
    public class ProfileWidget : Entity
    {
        #region Properties

        public virtual ProfileWidgetDisplayMode DisplayMode { get; set; }

        #endregion
    }
}