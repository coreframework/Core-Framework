using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;

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
            Map(formElement => formElement.Name).Length(255);
            Map(formElement => formElement.Type);
            Map(formElement => formElement.IsRequired);
            References(form => form.Form).Column("FormId").Nullable();
        }
    }
}
