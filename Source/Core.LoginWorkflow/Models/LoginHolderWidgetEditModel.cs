using Core.FormLogin.Models;
using Core.LoginWorkflow.NHibernate.Models;
using Core.OpenIDLogin.Models;
using Framework.Core.DomainModel;

namespace Core.LoginWorkflow.Models
{
    public class LoginHolderWidgetEditModel : IMappedModel<LoginHolderWidget, LoginHolderWidgetEditModel>
    {
        #region Properties

        public long Id { get; set; }        

        public LoginWidgetEditModel LoginWidgetEditModel { get; set; }

        public bool LoginWidgetShowTitle { get; set; }

        public OpenIDLoginWidgetEditModel OpenIDLoginWidgetEditModel { get; set; }

        public bool OpenIDLoginWidgetShowTitle { get; set; }

        #endregion

        #region Constructors

        #endregion

        #region Methods

        public LoginHolderWidgetEditModel MapFrom(LoginHolderWidget from)
        {
            Id = from.Id;
            LoginWidgetEditModel = new LoginWidgetEditModel { ChildView = true, ShowTitleFieldName = "LoginWidgetShowTitle" }.MapFrom(from.FormLoginWidget);
            LoginWidgetShowTitle = LoginWidgetEditModel.ShowTitle;
            OpenIDLoginWidgetEditModel = new OpenIDLoginWidgetEditModel { ChildView = true, ShowTitleFieldName = "OpenIDLoginWidgetShowTitle" }.MapFrom(from.OpenIdLoginWidget);
            OpenIDLoginWidgetShowTitle = OpenIDLoginWidgetEditModel.ShowTitle;

            return this;
        }

        public LoginHolderWidget MapTo(LoginHolderWidget to)
        {
            to.Id = Id;
            to.FormLoginWidget.ShowTitle = LoginWidgetShowTitle;
            to.OpenIdLoginWidget.ShowTitle = OpenIDLoginWidgetShowTitle;

            return to;
        }

        #endregion
    }
}