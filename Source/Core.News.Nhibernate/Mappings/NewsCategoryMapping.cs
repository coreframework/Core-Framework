using Core.News.Nhibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.News.Nhibernate.Mappings
{
    public class NewsCategoryMapping : ClassMap<NewsCategory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewsCategoryMapping"/> class.
        /// </summary>
        public NewsCategoryMapping()
         {
             Cache.Region("News_Categories").ReadWrite();
             Table("News_Categories");
             Id(category => category.Id);
             Map(category => category.CreateDate);

             HasMany(category => category.CurrentCategoryLocales).KeyColumn("CategoryId")
            .Table("News_CategoryLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
