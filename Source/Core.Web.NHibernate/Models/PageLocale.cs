using System;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Web.NHibernate.Models
{
    public class PageLocale : Entity, ILocale
    {
        public virtual Page Page { get; set; }

        public virtual String Culture { get; set; }

        public virtual String Title { get; set; }

        public virtual int Priority { get; private set; }
    }
}

