<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%if (Model.Id > 0) {%>
        <h3><%:Html.RouteLink(Html.Translate(".ViewFormElements"),
                                                new { controller = "Forms", action = "ShowFormElements", formId = Model.Id })%></h3>
   <% }%>
   <%: Html.ValidationSummary(true) %>
    <div class="form">
      <% using (Html.BeginForm(FormsMVC.Forms.Save(), FormMethod.Post))
         {%>    
          <div class="form_area">
              <%:Html.HiddenFor(model => model.Id)%>     
              <%:Html.TextBoxFor(model => model.Title)%>
              <%:Html.AntiForgeryToken()%>
         </div>
           <p class="buttons">
                  <%: Html.Submit("Save")%>
                    <%:Html.RouteLink("Cancel", new { controller = "Forms", action = "ShowAll" })%>
             </p>
          <% }%>
      </div>
</asp:Content>
