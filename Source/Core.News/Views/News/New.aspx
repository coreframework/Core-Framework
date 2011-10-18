<%@ Assembly Name="Core.News" %>
<%@ Import Namespace="Core.News.Models" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<NewsArticleViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>

<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%: String.Format(Html.Translate(".Title"), Model) %></h1>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <%: Html.ValidationSummary(true) %>
      <% using (Html.BeginForm())
         {%>    
    <div class="i_form clrfix">
	    <div class="cols clrfix">
            <div class="fst_col colls_i" style="width: 60%;">
			    <div class="i_form_i">
              <%:Html.EditorFor(model => model.Title)%>
                </div>
                <div class="i_form_i">
              <%:Html.EditorFor(model => model.Summary)%>
                </div>
			    <div class="i_form_i">
              <%:Html.EditorFor(model => model.Content)%>
                </div>    
                <%:Html.HiddenFor(model => model.PublishingAccess)%>            
                <%if (Model.PublishingAccess){%>
                <div class="i_form_i">
                    <%:Html.DropDownListFor(model => model.Status)%>
                </div>    
                <%}else{%>
                    <%:Html.HiddenFor(model => model.Status)%>
                <%} %>
                <div class="i_form_i">
                    <%:Html.EditorFor(model => model.PublishDate)%>
                </div>
			    <div class="i_form_i">
              <%:Html.AntiForgeryToken()%>
                </div>
            </div>
        </div>
		<div class="i_buttons clrfix">
			<div class="btn1 clrfix">
                <em></em>
                <%: Html.Submit(String.Format(Html.Translate(".Save"), Model),new { @class="button"})%>
                <strong></strong>
            </div>
			<span><%:Html.RouteLink(String.Format(Html.Translate(".Cancel"), Model), new { controller = "News", action = "ShowAll"})%></span>
		</div>
    </div>
          <% }%>
</asp:Content>