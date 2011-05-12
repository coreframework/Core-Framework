using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Core.Web.Helpers.PagedList;
using Core.Web.NHibernate.Models.Permissions;

namespace Core.Web.Helpers.HtmlExtensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality.
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// Toes the paged list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="allItems">All items.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public static PagedList<T> ToPagedList<T>(this ICollection<T> allItems, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);
            return new PagedList<T>(pageOfItems, pageIndex, pageSize);
        }

        public static String OperationCheckbox(this HtmlHelper helper, String name, long roleId, Int32 operationKey, IEnumerable<Permission> objectPermissions)
        {
            using (var stringWriter = new StringWriter())
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name,name);
                writer.AddAttribute(HtmlTextWriterAttribute.Value,String.Format("{0}_{1}",roleId,operationKey));
                writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");

                foreach (var objectPermission in objectPermissions)
                {
                    if ((objectPermission.Permissions & operationKey)==operationKey)
                    {
                         writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");

                        if (objectPermission.EntityId==null)
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                    }  
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();

                return stringWriter.ToString();
            }
        }
    }
}