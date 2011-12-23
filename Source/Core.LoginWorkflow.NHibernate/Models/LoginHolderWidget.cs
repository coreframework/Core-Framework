using Core.FormLogin.NHibernate.Models;
using Core.OpenIDLogin.NHibernate.Models;
using FluentNHibernate.Data;

namespace Core.LoginWorkflow.NHibernate.Models
{
    public class LoginHolderWidget : Entity
    {
        #region Properties

        public virtual FormLoginWidget FormLoginWidget { get; set; }

        public virtual OpenIDLoginWidget OpenIdLoginWidget { get; set; }

        #endregion

        #region Constructors

        public LoginHolderWidget()
        {
            FormLoginWidget = new FormLoginWidget();
            OpenIdLoginWidget = new OpenIDLoginWidget();
        }

        #endregion
    }
}
