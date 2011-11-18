<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Core.WebContent.Models.ArticleFileViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("EditArticle", "WebContent.Views.Article")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>
      <%:Html.Translate("EditArticle", "WebContent.Views.Article")%>
  </h1>
  <div class="tabs clrfix">
	<ul class="i-tab clrfix">
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Details", "WebContent.Views.Article"), "Edit")%>
            </span>
            <strong></strong>
        </li>
         <li class="active">
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Files", "WebContent.Views.Article"), "ShowFiles")%>
            </span>
            <strong></strong>
        </li>
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Permissions", "WebContent.Views.Article"), "ShowPermissions")%>
            </span>
            <strong></strong>
        </li>
	</ul>
  </div>
  <div class="tabs_b"></div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%: Html.ValidationSummary(true) %>
    <div class="i_form clrfix">
        <div class="cols clrfix">
            <div class="fst_col colls_i">
                <div class="i_form_i">
                 <% using (Html.BeginForm(WebContentMVC.Article.SaveFile(), FormMethod.Post))
                   {%> 
                   <div class="cols clrfix">
                      <%:Html.HiddenFor(model=>model.Id) %> 
                        <%:Html.HiddenFor(model=>model.ArticleId) %> 
                        <div class="i_form_i">
                            <%:Html.LocalizedLabelFor(model => model.Title)%>
                            <%:Html.TextBoxFor(model => model.Title)%>
                            <%:Html.ValidationMessageFor(model=>model.Title) %>
                        </div>
                        <div class="i_form_i">
                            <%:Html.EditorFor(model => model.FileName)%>
                        </div>
                     </div>
                      <div class="i_buttons clrfix">
                         <%if (Model.AllowManage){%>
		                    <div class="btn1 clrfix">
                                <em></em>
                                <%:Html.Submit(Html.Translate("Actions.Save"), new {@class = "button"})%>
                                <strong></strong>
                            </div>
                         <%}%>
	                    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Article", action = "ShowFiles" })%></span>
                    </div>
                 <% }%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>