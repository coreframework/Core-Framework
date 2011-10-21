using FluentNHibernate.Data;

namespace Core.News.Nhibernate.Models
{
    public class NewsDetailsWidget : Entity
    {
        #region Properties

        public virtual NewsDetailsLinkMode LinkMode { get; set; }

        #endregion
    }
}