<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.NavigationMenuModel>" %>

<%if (Model.Page!=null) {%>
    <%=Html.Hidden("menuPageId", Model.Page.Id) %>
    <%= Html.ActionLink(Html.Encode(Model.Page.Title), MVC.Pages.Show(Model.Page.Url)).ToString()%>
    <%if (!Model.IsCurrent && Model.RemoveAccess) {%>
        <span class="remove">
                <%= Ajax.ActionLink(" ", 
                                MVC.Pages.RemovePage(Model.Page.Id),
                                new AjaxOptions
                                            {
                                                Confirm = "Are you sure you want to delete this page?",
                                                OnSuccess = "function() {updateNavigationMenu(this);}"
                                            },
                                new { @class = "remove" })%>
        </span>
     <%} %>
<%} else {%>
        <a class="add-page" onclick="addNewPage('<%=Url.Action(MVC.Pages.CreateNewPage((Model.Parent != null && Model.Parent.Page != null) ? Model.Parent.Page.Id : 0))%>');return false;" href="javascript:void();"></a>
<%} %>
