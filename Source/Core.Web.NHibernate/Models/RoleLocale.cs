using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    public class RoleLocale : Entity, ILocale
    {
        public virtual Role Role { get; set; }

        public virtual String Culture { get; set; }

        public virtual String Name { get; set; }
    }
}
