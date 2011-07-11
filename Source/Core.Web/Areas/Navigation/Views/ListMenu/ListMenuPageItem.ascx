<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.ListMenuPageItemModel>" %>
<input type="checkbox" <%=Model.IsSelected?"checked=\"checked\"":"" %> name="PageIds" value="<%= Model.Page.Id %>" />
<%:Html.Encode(Model.Page.Title) %>

