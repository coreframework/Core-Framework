﻿using Core.Framework.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.Web.NHibernate.Mappings
{
    public class RoleLocaleMapping : ClassMap<RoleLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PluginLocaleMapping"/> class.
        /// </summary>
        public RoleLocaleMapping()
        {
            Cache.Region("RoleLocales").ReadWrite();
            Table("RoleLocales");
            Id(roleLocale => roleLocale.Id);
            References(roleLocale => roleLocale.Role).Column("RoleId").LazyLoad().Not.Nullable();
            Map(roleLocale => roleLocale.Culture).Length(5);
            Map(roleLocale => roleLocale.Name).Length(255);
            Map(widgetLocale => widgetLocale.Priority).Formula(CultureFilter.CultureFilterPriorityExpression());
        }
    }
}
