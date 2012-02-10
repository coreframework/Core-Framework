using Core.Framework.NHibernate.Models;
using FluentNHibernate.Data;
using Iesi.Collections.Generic;

namespace Core.Profiles.NHibernate.Models
{
    public class UserProfile: Entity
    {
        #region Fields

        private readonly ISet<UserProfileElement> profileElements = new HashedSet<UserProfileElement>();

        #endregion

        public virtual User User { get; set; }

        public virtual ProfileType ProfileType { get; set; }

        public virtual ISet<UserProfileElement> ProfileElements
        {
            get { return profileElements; }
        }

        public virtual void AddProfileElement(UserProfileElement element)
        {
            profileElements.Add(element);
        }
    }
}
