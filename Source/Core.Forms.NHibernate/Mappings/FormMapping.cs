using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Forms.NHibernate.Mappings
{
    /// <summary>
    /// Form entity mapping.
    /// </summary>
    public class FormMapping : ClassMap<Form>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormMapping"/> class.
        /// </summary>
        public FormMapping()
        {
            Cache.Region("Forms_Forms").ReadWrite();
            Table("Forms_Forms");
            Id(form => form.Id);
            Map(form => form.ShowSubmitButton);
            Map(form => form.ShowResetButton);
            Map(form => form.UserId);

            HasMany(form => form.CurrentFormLocales).KeyColumn("FormId")
            .Table("Forms_FormLocales").ApplyFilter<CultureFilter>()
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();

            HasMany(form => form.FormElements).KeyColumn("FormId")
           .Table("Forms_FormElements")
           .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
           .Inverse()
           .LazyLoad()
           .Cascade.AllDeleteOrphan();
        }
    }
}
