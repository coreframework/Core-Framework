<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.NavigationMenuItemModel>" %>
<%@ Import Namespace="Core.Web.Models" %>

<%if (Model.Page!=null) {%>
    <%=Html.Hidden("menuPageId", Model.Page.Id) %>
    <%= Html.ActionLink(String.IsNullOrEmpty(Model.Page.Title) ? " " : Html.Encode(Model.Page.Title), MVC.Pages.Show(Model.Page.Url), new {@class =  Model.IsCurrent?"current":""})%>
    <%if (!Model.IsCurrent && Model.RemoveAccess && Model.PageMode==PageMode.Edit) {%>
         <%= Ajax.ActionLink("    ", 
                                MVC.Pages.RemovePage(Model.Page.Id),
                                new AjaxOptions
                                            {
                                                Confirm = Html.Translate("Messages.DeleteConfirm"),
                                                OnSuccess = "function() {updateNavigationMenu(this);}"
                                            },
                                new { @class = "remove" })%>
     <%} %>
<%} else if (Model.PageMode == PageMode.Edit)
  {%> 
  <%if (Model.Parent!=null) {%>
    <img src="<%:Links.Content.Images.new_page_ico_png %>" width="12" height="12" alt="" title="" class="add_new" />
        <a class="add-page" onclick="addNewPage('<%=Url.Action(MVC.Pages.CreateNewPage((Model.Parent.Page != null) ? Model.Parent.Page.Id : 0))%>');return false;" href="javascript:void(0);">
            <%:Html.Translate("Actions.AddNewPage") %>
        </a>
   <%} else { %>
        <div class="btn2"><em></em> 
            <input type="button" class="button" value="<%:Html.Translate("Actions.AddNewPage") %>" onclick="addNewPage('<%=Url.Action(MVC.Pages.CreateNewPage(0))%>');return false;"/>
            <strong></strong>
        </div>
   <%} %>
<%} %>
