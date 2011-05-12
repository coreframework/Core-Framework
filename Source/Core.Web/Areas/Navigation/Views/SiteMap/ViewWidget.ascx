<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.Areas.Navigation.Models.SiteMapViewWidgetModel>>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions.MenuTreeView" %>

 <%=Html.RenderTree(Model, "site-map",
                                      model => Html.ActionLink(Html.Encode(model.Page.Title),MVC.Pages.Show(model.Page.Url)).ToString())%>