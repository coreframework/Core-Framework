﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.NavigationMenuItemModel>" %>
<%@ Import Namespace="Core.Web.Models" %>

<%if (Model.Page!=null) {%>
    <%=Html.Hidden("menuPageId", Model.Page.Id) %>
    <%= Html.ActionLink(Html.Encode(Model.Page.Title), MVC.Pages.Show(Model.Page.Url)).ToString()%>
    <%if (!Model.IsCurrent && Model.RemoveAccess && Model.PageMode==PageMode.Edit) {%>
         <%= Ajax.ActionLink("    ", 
                                MVC.Pages.RemovePage(Model.Page.Id),
                                new AjaxOptions
                                            {
                                                Confirm = "Are you sure you want to delete this page?",
                                                OnSuccess = "function() {updateNavigationMenu(this);}"
                                            },
                                new { @class = "remove" })%>
     <%} %>
<%} else if (Model.PageMode == PageMode.Edit)
  {%> 
  <%if (Model.Parent!=null) {%>
    <img src="<%:Links.Content.Images.new_page_ico_png %>" width="12" height="12" alt="" title="" class="add_new" />
        <a class="add-page" onclick="addNewPage('<%=Url.Action(MVC.Pages.CreateNewPage((Model.Parent.Page != null) ? Model.Parent.Page.Id : 0))%>');return false;" href="javascript:void(0);">
            <%:Html.Translate(".AddNewPage") %>
        </a>
   <%} else { %>
        <div class="btn2"><em></em> 
            <input type="button" class="button" value="<%:Html.Translate(".AddNewPage") %>" onclick="addNewPage('<%=Url.Action(MVC.Pages.CreateNewPage(0))%>');return false;"/>
            <strong></strong>
        </div>
   <%} %>
<%} %>
