using FluentNHibernate.Data;

namespace Core.ContentPages.NHibernate.Models
{
    public class ContentPageWidget : Entity
    {
        public virtual ContentPage ContentPage { get; set; }
    }
}