<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.OpenIDLogin.Models.OpenIDLoginWidgetEditModel>" %>

<%@ Import Namespace="Framework.Mvc.Extensions" %>
<div class="form_area">
    <%if (!Model.ChildView)
      {%>
    <%:Html.ValidationSummary(true)%>
    <%:Html.Messages()%>
    <input type="hidden" id="widgetId" name="widgetId" value="<%=Html.Encode(Model.Id)%>" />
    <input type="hidden" id="Id" name="Id" value="<%=Html.Encode(Model.Id)%>" />
    <%
      }%>
    <div class="form_i" style="margin: 0;">
        <% if (String.IsNullOrEmpty(Model.ShowTitleFieldName))
           {%>
        <%:Html.CheckBoxFor(model => model.ShowTitle)%>
        <%
       }
           else
           {%>
        <%:Html.CheckBox(Model.ShowTitleFieldName, Model.ShowTitle)%>
        <%
       }%>
        <%:Html.LocalizedLabelFor(model => model.ShowTitle, new { Class = "checkbx-label" })%>
    </div>
    <%:Html.AntiForgeryToken()%>
</div>
