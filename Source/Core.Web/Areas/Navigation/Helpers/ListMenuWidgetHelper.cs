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
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Areas.Navigation.Helpers
{
    public class ListMenuWidgetHelper
    {
        /// <summary>
        /// Binds the widget model.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public static ListMenuWidget BindWidgetModel(ICoreWidgetInstance instance, ICorePrincipal user)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IListMenuWidgetService>();
            var pageService = ServiceLocator.Current.GetInstance<IPageService>();

            var widget =  widgetService.Find(instance.InstanceId ?? 0);
            if (widget!=null)
            {
                var allowedPages = pageService.GetAllowedPagesByOperation(user, (int)PageOperations.View);
                widget.Pages = widget.Pages.Where(page => allowedPages.Contains(page));
            }
            
            return widget;
        }

        /// <summary>
        /// Binds the list menu pages.
        /// </summary>
        /// <param name="selectedPages">The selected pages.</param>
        /// <param name="pages">The pages.</param>
        /// <returns></returns>
        public static IEnumerable<ListMenuPageItemModel> BindListMenuPages(IEnumerable<Page> selectedPages,IEnumerable<Page> pages)
        {
           
            pages = pages.OrderBy(page => page.OrderNumber);

            List<ListMenuPageItemModel> items = pages.Select(page => new ListMenuPageItemModel { Page = page }).ToList();

            foreach (var item in items)
            {
                if (item.Page.ParentPageId == null)
                {
                    item.Children = Flatten(item, items,selectedPages);
                    item.IsSelected = selectedPages != null && selectedPages.FirstOrDefault(page=>page.Id==item.Page.Id) != null;
                    yield return item;
                }
            }
        }

        public static IEnumerable<ListMenuPageItemModel> Flatten(ListMenuPageItemModel root, List<ListMenuPageItemModel> items, IEnumerable<Page> selectedItems)
        {
            var children = items.Where(item => item.Page.ParentPageId == root.Page.Id).ToList();
            foreach (var child in children)
            {
                child.Children = Flatten(child, items,selectedItems);
                child.IsSelected = selectedItems != null && selectedItems.FirstOrDefault(page => page.Id == child.Page.Id) != null;
                yield return child;
            }
        }

        public static ListMenuWidgetModel SaveListMenuWidget(ListMenuWidgetModel model)
        {
            var widgetService = ServiceLocator.Current.GetInstance<IListMenuWidgetService>();
            var widget = model.MapTo(new ListMenuWidget());
            widgetService.Save(widget);
            return new ListMenuWidgetModel().MapFrom(widget);
        }
    }
}