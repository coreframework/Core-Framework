using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    public class WidgetLocale : Entity, ILocale
    {
        public virtual Widget Widget { get; set; }

        public virtual String Culture { get; set; }

        public virtual String Title { get; set; }

        public virtual int Priority { get; private set; }
    }
}
