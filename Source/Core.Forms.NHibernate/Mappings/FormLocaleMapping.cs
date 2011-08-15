using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Forms.NHibernate.Mappings
{
    /// <summary>
    /// Form entity mapping.
    /// </summary>
    public class FormLocaleMapping : ClassMap<FormLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormLocaleMapping"/> class.
        /// </summary>
        public FormLocaleMapping()
        {
            Cache.Region("Forms_FormLocales").ReadWrite();
            Table("Forms_FormLocales");
            Id(formlocale => formlocale.Id);
            Map(formlocale => formlocale.Title);
            Map(formlocale => formlocale.Culture);
            Map(formlocale => formlocale.SubmitButtonText).Length(255);
            Map(formlocale => formlocale.ResetButtonText).Length(255);
            References(formlocale => formlocale.Form).Column("FormId").LazyLoad().Not.Nullable();
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
