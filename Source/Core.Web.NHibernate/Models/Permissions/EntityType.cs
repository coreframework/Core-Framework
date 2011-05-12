using System;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models.Permissions
{
    public class EntityType: Entity
    {
        public virtual String Name { get; set; }
    }
}
