using FluentNHibernate.Data;

namespace Core.WebContent.NHibernate.Models
{
    public class WebContentDetailsWidget : Entity
    {
        #region Properties

        public virtual WebContentDetailsLinkMode LinkMode { get; set; }

        #endregion
    }
}