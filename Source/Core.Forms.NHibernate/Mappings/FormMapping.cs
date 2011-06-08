using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;

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
            Map(form => form.Title).Length(255);
            Map(form => form.UserId);

            HasMany(form => form.FormElements).KeyColumn("FormId")
              .Table("Forms_FormElements")
              .LazyLoad()
              .Cascade.AllDeleteOrphan();
        }
    }
}
