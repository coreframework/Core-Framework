using Core.LoginWorkflow.NHibernate.Models;
using FluentNHibernate.Mapping;

namespace Core.LoginWorkflow.NHibernate.Mappings
{
    public class LoginHolderWidgetMapping : ClassMap<LoginHolderWidget>
    {
        public LoginHolderWidgetMapping()
        {
            Cache.Region("LoginWorkflow_LoginHolderWidget").ReadWrite();
            Table("LoginWorkflow_LoginHolderWidget");
            Id(formLoginWidget => formLoginWidget.Id);
            References(formLoginWidget => formLoginWidget.FormLoginWidget).Column("FormLoginWidgetId").Cascade.All();
            References(formBuilderWidget => formBuilderWidget.OpenIdLoginWidget).Column("OpenIdLoginWidgetId").Cascade.All();
        }
    }
}