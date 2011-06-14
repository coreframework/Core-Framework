<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#"  Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormElementViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%: Html.ValidationSummary(true) %>
    <div class="form">
      <% using (Html.BeginForm(FormsMVC.Forms.Save(), FormMethod.Post))
         {%>    
          <div class="form_area">
              <%=Html.HiddenFor(model => model.Id)%>     
              <%=Html.TextBoxFor(model => model.Title)%>
              <%=Html.DropDownListFor("Type", Model.Type, new {})%>
              <%=Html.CheckBoxFor(model => model.IsRequired)%>

              <%=Html.AntiForgeryToken()%>
         </div>
           <p class="buttons">
                  <%: Html.Submit("Save")%>
                  <%:Html.RouteLink("Cancel", new { controller = "Forms", action = "ShowAll" })%>
             </p>
          <% }%>
      </div>
</asp:Content>
