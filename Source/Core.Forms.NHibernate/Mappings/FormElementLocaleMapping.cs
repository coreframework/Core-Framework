using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Forms.NHibernate.Mappings
{
    /// <summary>
    /// Form entity mapping.
    /// </summary>
    public class FormElementLocaleMapping : ClassMap<FormElementLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormElementLocaleMapping"/> class.
        /// </summary>
        public FormElementLocaleMapping()
        {
            Cache.Region("Forms_FormElementLocales").ReadWrite();
            Table("Forms_FormElementLocales");
            Id(formElementLocale => formElementLocale.Id);
            Map(formElementLocale => formElementLocale.Title).Length(255);
            Map(formElementLocale => formElementLocale.Culture);
            Map(formElementLocale => formElementLocale.ElementValues);
            References(formElementLocale => formElementLocale.FormElement).Column("FormElementId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
