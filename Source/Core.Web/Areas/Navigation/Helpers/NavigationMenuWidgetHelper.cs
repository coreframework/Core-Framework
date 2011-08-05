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
    public class NavigationMenuWidgetHelper
    {
       
        /// <summary>
        /// Gets the navigation menu.
        /// </summary>
        /// <returns></returns>
        public static PartialNavigationMenuModel GetNavigationMenu(ICorePrincipal user, NavigationMenuWidget widget)
        {
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            var pages = pageService.GetAllowedPagesByOperation(user, (int)PageOperations.View).OrderBy(page => page.OrderNumber);
          
            var menuItems = new List<PartialNavigationMenuItemModel>();

            List<PartialNavigationMenuItemModel> items = pages.Select(page => new PartialNavigationMenuItemModel
            {
                Page = page
            }).ToList();

            foreach (var item in items)
            {
                if (item.Page.ParentPageId == null)
                {
                    item.Children = Flatten(item, items, 2);
                    menuItems.Add(item);
                }
            }

            return new PartialNavigationMenuModel
            {
                MenuItems = menuItems,
                WidgetId = widget.Id,
                Orientation = widget.Orientation
            };
        }

        public static List<PartialNavigationMenuItemModel> Flatten(PartialNavigationMenuItemModel root, List<PartialNavigationMenuItemModel> items, int level)
        {
            var flattened = new List<PartialNavigationMenuItemModel>();

            var children = items.Where(item => item.Page.ParentPageId != null && item.Page.ParentPageId == root.Page.Id).ToList();

            foreach (var child in children)
            {
                child.Children = Flatten(child, items, level + 1);
                flattened.Add(child);
            }

            return flattened;
        }

        /// <summary>
        /// Saves the navigation menu widget.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public static NavigationMenuWidgetModel SaveNavigationMenuWidget(NavigationMenuWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INavigationMenuWidgetService>();
            var widget = model.MapTo(new NavigationMenuWidget());
            widgetService.Save(widget);
            return new NavigationMenuWidgetModel().MapFrom(widget);
        }

        /// <summary>
        /// Clones the navigation menu widget.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static long? CloneNavigationMenuWidget(ICoreWidgetInstance instance)
        {
            var widgetService = ServiceLocator.Current.GetInstance<INavigationMenuWidgetService>();
            var widget = widgetService.Find(instance.InstanceId ?? 0);

            if (widget != null)
            {
                var clone = (NavigationMenuWidget)new NavigationMenuWidget().InjectFrom<CloneEntityInjection>(widget);
                if (widgetService.Save(clone))
                {
                    return clone.Id;
                }
            }
            return null;
        }
    }
}