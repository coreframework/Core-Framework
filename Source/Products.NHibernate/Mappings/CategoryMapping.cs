using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;
using Products.NHibernate.Models;

namespace Products.NHibernate.Mappings
{
    public class CategoryMapping : ClassMap<Category>
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMapping"/> class.
        /// </summary>
        public CategoryMapping()
         {
             Cache.Region("Product_Categories").ReadWrite();
             Table("Product_Categories");
             Id(category => category.Id);
             Map(category => category.CreateDate);
            
             HasMany(category => category.CurrentCategoryLocales).KeyColumn("CategoryId")
            .Table("Product_CategoryLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
