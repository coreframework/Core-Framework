using System.Collections.Generic;
using System.Linq;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Navigation.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models.Widgets;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

namespace Core.Web.Areas.Navigation.Helpers
{
    public static class SiteMapWidgetHelper
    {
        #region Helper Methods

        /// <summary>
        /// Binds the site map.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static IEnumerable<SiteMapViewWidgetModel> BindSiteMap(ICoreWidgetInstance instance, ICorePrincipal user)
        {
            var resultList = new List<SiteMapViewWidgetModel>();
            var widgetService = ServiceLocator.Current.GetInstance<ISiteMapWidgetService>();

            var widgetInstance = widgetService.Find(instance.InstanceId ?? 0);

            if (widgetInstance!=null)
            {
                var pageService = ServiceLocator.Current.GetInstance<IPageService>();

                var pages = pageService.GetAllowedPagesByOperation(user,(int)PageOperations.View).OrderBy(page => page.OrderNumber);

                List<SiteMapViewWidgetModel> items = pages.Select(page => new SiteMapViewWidgetModel { Page = page }).ToList();

                if (widgetInstance.RootPage==null)
                {
                    foreach (var item in items)
                    {
                        if (item.Page.ParentPageId == null)
                        {
                            item.Children = Flatten(item, items, 2,widgetInstance.Depth);
                            resultList.Add(item);
                        }
                    }
                }
                else
                {
                    var currentPage = items.Find(page => page.Page.Id == widgetInstance.RootPage.Id);
                    if (currentPage!=null)
                    {
                        if (widgetInstance.IncludeRootInTree)
                        {
                            currentPage.Children = Flatten(currentPage, items,2,widgetInstance.Depth);
                            resultList.Add(currentPage);
                        }
                        else
                        {
                            resultList =  Flatten(currentPage, items, 1, widgetInstance.Depth);
                        }
                     
                    }
                }
            }
            return resultList;
        }

        public static List<SiteMapViewWidgetModel> Flatten(SiteMapViewWidgetModel root, List<SiteMapViewWidgetModel> items, int level,int? maxLevel)
        {
            var flattened = new List<SiteMapViewWidgetModel> ();

            if (maxLevel==null || maxLevel>=level)
            {
                var children = items.Where(item => item.Page.ParentPageId == root.Page.Id).ToList();
                foreach (var child in children)
                {
                    child.Children = Flatten(child, items, level + 1, maxLevel);
                    flattened.Add(child);
                }
            }
          
            return flattened;
        }

        /// <summary>
        /// Saves the site map widget widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static SiteMapWidgetModel SaveSiteMapWidget(SiteMapWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<ISiteMapWidgetService>();
            var widget = model.MapTo(new SiteMapWidget());
            widgetService.Save(widget);
            return new SiteMapWidgetModel().MapFrom(widget);
        }

        /// <summary>
        /// Clones the site map widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static long? CloneSiteMapWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<ISiteMapWidgetService>();

            if (instance.InstanceId!=null)
            {
                var widget = widgetService.Find((long)instance.InstanceId);

                if (widget != null)
                {
                    var clone = (SiteMapWidget)new SiteMapWidget().InjectFrom<CloneEntityInjection>(widget);

                    if (widgetService.Save(clone))
                    {
                        return clone.Id;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}