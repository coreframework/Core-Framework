using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Forms.NHibernate.Mappings
{
    public class FormElementMapping: ClassMap<FormElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormElementMapping"/> class.
        /// </summary>
        public FormElementMapping()
        {
            Cache.Region("Forms_FormElements").ReadWrite();
            Table("Forms_FormElements");
            Id(formElement => formElement.Id);
            Map(formElement => formElement.Type).CustomType(typeof(FormElementType)).Nullable();
            Map(formElement => formElement.OrderNumber);
            Map(formElement => formElement.IsRequired);
            Map(formElement => formElement.MaxLength);
            Map(formElement => formElement.RegexTemplate).CustomType(typeof(RegexTemplate));
            References(form => form.Form).Column("FormId");

            HasMany(formElement => formElement.CurrentLocales).KeyColumn("FormElementId")
             .Table("Forms_FormElementLocales").ApplyFilter<CultureFilter>()
             .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
             .Inverse()
             .LazyLoad()
             .Cascade.All();
        }
    }
}
