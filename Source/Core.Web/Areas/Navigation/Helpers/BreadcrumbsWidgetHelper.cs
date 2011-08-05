using System.Collections.Generic;
using System.Linq;
using Core.Framework.Permissions.Models;
using Core.Framework.Plugins.Web;
using Core.Web.Areas.Navigation.Models;
using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Contracts.Widgets;
using Core.Web.NHibernate.Models;
using Core.Web.NHibernate.Models.Widgets;
using Core.Web.NHibernate.Permissions.Operations;
using Framework.Core.Extensions;
using Microsoft.Practices.ServiceLocation;
using Omu.ValueInjecter;

namespace Core.Web.Areas.Navigation.Helpers
{
    public class BreadcrumbsWidgetHelper
    {
        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static IEnumerable<BreadcrumbsItem> BindWidgetModel(ICoreWidgetInstance instance, ICorePrincipal user)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IBreadcrumbsWidgetService>();
            var breadcrumbsPages = new List<BreadcrumbsItem>();

            BreadcrumbsWidget widget = widgetService.Find(instance.InstanceId ?? 0);
            
            if (widget != null && instance.PageSettings != null)
            {
                var pageService = ServiceLocator.Current.GetInstance<IPageService>();

                IEnumerable<Page> pages = pageService.GetAllowedPagesByOperation(user, (int) PageOperations.View);

                Page currentPage = pages.FirstOrDefault(pg => pg.Id == instance.PageSettings.PageId);

                if (currentPage != null)
                {
                    Page activePage = currentPage;

                    while (activePage != null)
                    {
                        breadcrumbsPages.Add(new BreadcrumbsItem {Title = activePage.Title, Url = activePage.Url});
                        Page page = activePage;
                        activePage = pages.FirstOrDefault(pg => pg.Id == page.ParentPageId);
                    }
                    if (widget.ShowHomePage)
                        breadcrumbsPages.Add(new BreadcrumbsItem {Title = "Core Framework" ,IsHomePage = true});
                    breadcrumbsPages.Reverse();
                }
            }
            return breadcrumbsPages;
        }

        /// <summary>
        /// Saves the breadcrumbs widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static BreadcrumbsWidgetModel SaveBreadcrumbsWidget(BreadcrumbsWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IBreadcrumbsWidgetService>();
            var widget = model.MapTo(new BreadcrumbsWidget());
            widgetService.Save(widget);
            return new BreadcrumbsWidgetModel().MapFrom(widget);
        }

        /// <summary>
        /// Clones the breadcrumbs widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static long? CloneBreadcrumbsWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IBreadcrumbsWidgetService>();
            var widget = widgetService.Find(instance.InstanceId ?? 0);

            if (widget != null)
            {
                var clone = (BreadcrumbsWidget)new BreadcrumbsWidget().InjectFrom<CloneEntityInjection>(widget);
                if (widgetService.Save(clone))
                {
                    return clone.Id;
                }
            }
            return null;
        }
    }
}