using System.Collections.Generic;
using Core.Framework.NHibernate.Models;
using FluentNHibernate.Data;

namespace Core.Profiles.NHibernate.Models
{
    public class UserProfile: Entity
    {
        #region Fields

        private readonly IList<UserProfileElement> profileElements = new List<UserProfileElement>();

        #endregion

        public virtual User User { get; set; }

        public virtual ProfileType ProfileType { get; set; }

        public virtual IList<UserProfileElement> ProfileElements
        {
            get { return profileElements; }
        }

        public virtual void AddProfileElement(UserProfileElement element)
        {
            profileElements.Add(element);
        }
    }
}
