using Core.Forms.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.Forms.NHibernate.Mappings
{
    public class FormBuilderWidgetMapping: ClassMap<FormBuilderWidget>
    {
        public FormBuilderWidgetMapping()
        {
            Cache.Region("Forms_FormsBuilderWidgets").ReadWrite();
            Table("Forms_FormsBuilderWidgets");
            Id(formBuilderWidget => formBuilderWidget.Id);
            Map(formBuilderWidget => formBuilderWidget.Title).Length(255);
            Map(formBuilderWidget => formBuilderWidget.SaveData);
            Map(formBuilderWidget => formBuilderWidget.SendEmail);
            Map(formBuilderWidget => formBuilderWidget.RecipientEmail);
            References(formBuilderWidget => formBuilderWidget.User).Column("UserId");
            References(formBuilderWidget => formBuilderWidget.Form).Column("FormId");

            HasMany(formBuilderWidget => formBuilderWidget.Answers).KeyColumn("FormWidgetId")
             .Table("Forms_FormsBuilderWidgets")
             .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.None)
             .Inverse()
             .LazyLoad()
             .Cascade.AllDeleteOrphan();
        }
    }
}
