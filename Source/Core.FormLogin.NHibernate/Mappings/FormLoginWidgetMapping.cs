using Core.FormLogin.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.FormLogin.NHibernate.Mappings
{
    public class FormLoginWidgetMapping : ClassMap<FormLoginWidget>
    {
        public FormLoginWidgetMapping()
        {
            Cache.Region("FormLogin_FormLoginWidgets").ReadWrite();
            Table("FormLogin_FormLoginWidgets");
            Id(formLoginWidget => formLoginWidget.Id);
            Map(formLoginWidget => formLoginWidget.ShowTitle);
        }
    }
}