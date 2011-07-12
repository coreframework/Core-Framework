﻿using System;
using Core.ContentPages.NHibernate.Models;
using FluentNHibernate.Mapping;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Filters;

namespace Core.ContentPages.NHibernate.Mappings
{
    public class ContentPageMapping : ClassMap<ContentPage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentPageMapping"/> class.
        /// </summary>
        public ContentPageMapping()
        {
            Cache.Region("ContentPages").ReadWrite();
            Table("ContentPages");
            Id(contentPage => contentPage.Id);
            Map(contentPage => contentPage.Title).Length(255);
            Map(contentPage => contentPage.Content);

            HasMany(page => page.Widgets).KeyColumn("ContentPageId")
            .Table("ContentPageWidgets")
            .Inverse()
            .LazyLoad()
            .Cascade.All();
            HasMany(contentPage => contentPage.CurrentContentPageLocales).KeyColumn("ContentPageId")
            .Table("ContentPageLocales").ApplyFilter<CultureFilter>(String.Format("Culture = {0}", CultureFilter.FilterParamNameForQuery))
            .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
            .Inverse()
            .LazyLoad()
            .Cascade.All();
        }
    }
}
