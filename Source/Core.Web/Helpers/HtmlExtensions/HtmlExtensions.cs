using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using Core.Framework.Permissions.Models;

namespace Core.Web.Helpers.HtmlExtensions
{
    /// <summary>
    /// Extends <see cref="HtmlHelper"/> functionality.
    /// </summary>
    public static class HtmlExtensions
    {
        public static String OperationCheckbox(this HtmlHelper helper, String name, long roleId, Int32 operationKey, IEnumerable<IPermissionModel> objectPermissions)
        {
            using (var stringWriter = new StringWriter())
            using (var writer = new HtmlTextWriter(stringWriter))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Name, name);
                writer.AddAttribute(HtmlTextWriterAttribute.Value, String.Format("{0}_{1}",roleId,operationKey));
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