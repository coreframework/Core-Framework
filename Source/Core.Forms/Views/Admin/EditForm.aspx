<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.Forms.Models.FormViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>Edit Form</h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%if (Model.Id > 0) {%>
        <h3><%:Html.RouteLink(Html.Translate(".ViewFormElements"),
                                                new { controller = "Forms", action = "ShowFormElements", formId = Model.Id })%></h3>
   <% }%>
   <%: Html.ValidationSummary(true) %>
      <% using (Html.BeginForm(FormsMVC.Forms.Save(), FormMethod.Post))
         {%>    
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i">
              <%:Html.HiddenFor(model => model.Id)%>     
			    <div class="i_form_i">
              <%:Html.EditorFor(model => model.Title)%>
                </div>
			    <div class="i_form_i">
              <%:Html.AntiForgeryToken()%>
                </div>
            </div>
        </div>
		<div class="i_buttons clrfix">
			<div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit("Save",new { @class="button"})%>
                <strong></strong>
            </div>
			<span><%:Html.RouteLink("Cancel", new { controller = "Forms", action = "ShowAll" })%></span>
		</div>
    </div>
          <% }%>
</asp:Content>
