using Core.OpenIDLogin.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.OpenIDLogin.NHibernate.Mappings
{
    public class OpenIDLoginWidgetMapping : ClassMap<OpenIDLoginWidget>
    {
        public OpenIDLoginWidgetMapping()
        {
            Cache.Region("OpenIDLogin_OpenIDLoginWidgets").ReadWrite();
            Table("OpenIDLogin_OpenIDLoginWidgets");
            Id(formLoginWidget => formLoginWidget.Id);
            Map(formLoginWidget => formLoginWidget.ShowTitle);
        }
    }
}