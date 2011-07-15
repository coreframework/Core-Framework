using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    public class PluginLocale : Entity, ILocale
    {
        public virtual Plugin Plugin { get; set; }

        public virtual String Culture { get; set; }

        public virtual String Title { get; set; }
        
        public virtual String Description { get; set; }
    }
}
