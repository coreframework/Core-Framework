using Core.Web.NHibernate.Contracts;
using Core.Web.NHibernate.Models;
using Microsoft.Practices.ServiceLocation;
using System.Linq;

namespace Core.Web.Helpers.Layouts
{
    public static class LayoutHelper
    {
        #region Methods

        /// <summary>
        /// Changes the page layout.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="fromLayoutTemplate">From layout template.</param>
        /// <param name="toLayoutTemplate">To layout template.</param>
        public static void ChangePageLayout(Page page, PageLayoutTemplate fromLayoutTemplate,
                                            PageLayoutTemplate toLayoutTemplate)
        {
            if (fromLayoutTemplate == null || toLayoutTemplate == null)
                return;

            page.PageLayout.LayoutTemplate = toLayoutTemplate;
            if (fromLayoutTemplate.ColumnsNumber > toLayoutTemplate.ColumnsNumber)
            {
                page.Widgets.Update(
                    wd =>
                    {
                        wd.OrderNumber =
                            (wd.ColumnNumber > toLayoutTemplate.ColumnsNumber
                                 ? page.Widgets.Where(w => w.ColumnNumber == toLayoutTemplate.ColumnsNumber).Count
                                       () + 1
                                 : wd.OrderNumber);
                        wd.ColumnNumber =
                            (wd.ColumnNumber > toLayoutTemplate.ColumnsNumber
                                 ? toLayoutTemplate.ColumnsNumber
                                 : wd.ColumnNumber);

                    }
                    );
            }

            var pageService = ServiceLocator.Current.GetInstance<IPageService>();
            pageService.Save(page);
        }

        public static PageLayoutTemplate GetDefaultLayoutTemplate()
        {
            IPageLayoutTemplateService pageLayoutTemplateService =
                ServiceLocator.Current.GetInstance<IPageLayoutTemplateService>();
            return pageLayoutTemplateService.FindDefault();
        }

        public static int GetColumnWidth(PageLayout layout, PageLayoutColumn column)
        {
            PageLayoutColumnWidthValue columnWidthValue =
                layout.ColumnWidths.FirstOrDefault(columnWidths => columnWidths.Column == column);
            return columnWidthValue != null ? columnWidthValue.WidthValue : column.DefaultWidthValue;
        }

        public static int GetColumnColspan(PageLayout layout, PageLayoutColumn column)
        {
            int colspan = 1;
            PageLayoutColumnWidthValue columnWidthValue =
                layout.ColumnWidths.FirstOrDefault(columnWidths => columnWidths.Column == column);
            if (columnWidthValue != null && columnWidthValue.Colspan.HasValue)
            {
                colspan = columnWidthValue.Colspan.Value;
            }
            else if (column.DefaultColspan.HasValue)
            {
                colspan = column.DefaultColspan.Value;
            }
            return colspan;
        }

        #endregion
    }
} ;