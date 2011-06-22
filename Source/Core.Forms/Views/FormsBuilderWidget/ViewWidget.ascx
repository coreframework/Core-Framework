<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Import Namespace="Core.Forms.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.NHibernate.Models.FormBuilderWidget>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
<%@ Import Namespace="Core.Forms.Extensions" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <fieldset>
    <h1><%:Model.Title%></h1>
    <%using (Html.BeginForm())
      {%>
         <%
          foreach (var item in Model.Form.FormElements)
          {%>
            <div>
                <%=Html.FormElementRenderer(item)%>
           </div>
           <div style="clear:both;"></div>
         <%
          }%>
         <%
      }%>
    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>

