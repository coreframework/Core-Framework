﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.PageViewModel>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<%@ Import Namespace="Core.Web.Helpers.Layouts" %>
<%@ Import Namespace="Core.Framework.Plugins.Widgets" %>
<%@ Import Namespace="Core.Web.NHibernate.Models.Static" %>
<% int columnIndex = 1;
   if (Model.TemplateModel == null)
   {%>
<%
       foreach (PageLayoutRow row in Model.Layout.LayoutTemplate.Rows)
       {%>
<table class="widgets">
    <tr rowid="<%=row.Id%>">
        <%
           foreach (var column in row.Columns)
           {%>
        <td style="width: <%=LayoutHelper.GetColumnWidth(Model.Layout, column)%>%;" colspan="<%=LayoutHelper.GetColumnColspan(Model.Layout, column)%>"
            class="column">
            <%
               foreach (
                   var widget in
                       Model.Widgets.FindAll(
                           wd => wd.Widget.PageSection == PageSection.Body && wd.Widget.ColumnNumber == columnIndex))
               {%>
            <%
                   if (widget.Widget == null || !(widget.SystemWidget is BaseWidget) ||
                       widget.Access[((BaseWidget)widget.SystemWidget).ViewOperationCode])
                   {%>
            <%
                       Html.RenderPartial(
                           MVC.Shared.Views.Widgets.WidgetContentHolder,
                           widget);%>
            <%
                   }%>
            <%
               }%>
        </td>
        <%
               columnIndex++;
           }
        %>
    </tr>
</table>
<%
       }
   }
   else
   {
       foreach (PageLayoutRow row in Model.TemplateModel.Layout.LayoutTemplate.Rows)
       {%>
       <table class="widgets">
    <tr rowid="<%=row.Id%>">
        <%
           foreach (var column in row.Columns)
           {%>
        <td style="width: <%=LayoutHelper.GetColumnWidth(Model.TemplateModel.Layout, column)%>%;" colspan="<%=LayoutHelper.GetColumnColspan(Model.TemplateModel.Layout, column)%>"
            class="column">
            <%
               foreach (
                   var widget in
                       Model.TemplateModel.Widgets.FindAll(
                           wd => wd.Widget.PageSection == PageSection.Body && wd.Widget.ColumnNumber == columnIndex))
               {%>
            <%
                   if (widget.Widget == null || !(widget.SystemWidget is BaseWidget) ||
                       widget.Access[((BaseWidget)widget.SystemWidget).ViewOperationCode])
                   {%>
            <%
                       Html.RenderPartial(
                           MVC.Shared.Views.Widgets.WidgetContentHolder,
                           widget);%>
            <%
                   }%>
            <%
               }%>
        </td>
        <%
               columnIndex++;
           }
        %>
    </tr>
</table>
<%
       }
   } %>
