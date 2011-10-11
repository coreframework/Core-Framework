using Core.Web.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Web.NHibernate.Mappings
{
    public class PageLocaleMapping : ClassMap<PageLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageLocaleMapping"/> class.
        /// </summary>
        public PageLocaleMapping()
        {
            Cache.Region("PageLocales").ReadWrite();
            Table("PageLocales");
            Id(pageLocale => pageLocale.Id);
            References(pageLocale => pageLocale.Page).Column("PageId").LazyLoad().Not.Nullable();
            Map(pageLocale => pageLocale.Culture).Length(5);
            Map(pageLocale => pageLocale.Title).Length(255);
            Map(pageLocale => pageLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
