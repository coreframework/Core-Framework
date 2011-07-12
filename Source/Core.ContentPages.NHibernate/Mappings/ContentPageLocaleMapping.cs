using System;
using Core.ContentPages.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Facilities.NHibernate.Filters;

namespace Core.ContentPages.NHibernate.Mappings
{
    public class ContentPageLocaleMapping : ClassMap<ContentPageLocale>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageLocaleMapping"/> class.
        /// </summary>
        public ContentPageLocaleMapping()
        {
            Cache.Region("ContentPageLocales").ReadWrite();
            Table("ContentPageLocales");
            Id(contentPageLocale => contentPageLocale.Id);
            References(contentPageLocale => contentPageLocale.ContentPage).Column("ContentPageId").LazyLoad().Not.Nullable();
            Map(contentPageLocale => contentPageLocale.Culture).Length(5);
            Map(contentPageLocale => contentPageLocale.Title).Length(255);
            Map(contentPageLocale => contentPageLocale.Content);
        }
    }
}
