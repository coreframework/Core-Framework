using FluentNHibernate.Data;

namespace Core.OpenIDLogin.NHibernate.Models
{
    public  class OpenIDLoginWidget : Entity
    {
        #region Properties

        public virtual bool ShowTitle { get; set; }

        #endregion

        #region Constructors

        public OpenIDLoginWidget()
        {
            ShowTitle = true;
        }

        #endregion
    }
}
