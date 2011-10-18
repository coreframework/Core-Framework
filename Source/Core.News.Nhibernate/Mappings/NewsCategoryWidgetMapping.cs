using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsCategoryWidgetMapping : ClassMap<NewsCategoryWidget>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsCategoryWidgetMapping"/> class.
        /// </summary>
        public NewsCategoryWidgetMapping()
        {
            Cache.Region("News_CategoryWidgets").ReadWrite();
            Table("News_CategoryWidgets");
            Id(categoryWidgets => categoryWidgets.Id);
            Map(categoryWidgets => categoryWidgets.PageSize);
        }
    }
}
