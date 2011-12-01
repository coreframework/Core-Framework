using FluentNHibernate.Data;

namespace Core.Profiles.NHibernate.Models
{
    public class RegistrationWidget : Entity
    {
        #region Properties

        public virtual ProfileType ProfileType { get; set; }

        #endregion

    }
}