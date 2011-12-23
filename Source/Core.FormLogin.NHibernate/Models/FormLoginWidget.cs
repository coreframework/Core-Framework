using FluentNHibernate.Data;

namespace Core.FormLogin.NHibernate.Models
{
    public class FormLoginWidget : Entity
    {
        #region Properties

        public virtual bool ShowTitle { get; set; }

        #endregion

        #region Constructors

        public FormLoginWidget()
        {
            ShowTitle = true;
        }

        #endregion
    }
}
