using System;
using FluentNHibernate.Data;

namespace Core.Profiles.NHibernate.Models
{
    public class UserProfileElement: Entity
    {
        public virtual UserProfile UserProfile { get; set; }

        public virtual ProfileElement ProfileElement { get; set; }

        public virtual String Value { get; set; }
    }
}
