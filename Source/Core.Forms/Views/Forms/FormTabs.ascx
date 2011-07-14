<%@ Assembly Name="Core.Forms" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Core.Forms.Models.MenuItemModel>>" %>
<div class="tabs clrfix">
	<ul class="i-tab clrfix">
    <%foreach (var item in Model) {%>
       <li <%=item.IsActive?"class=\"active\"":"" %>><em></em><span><a href="<%=item.Url %>"><%:Html.Encode(item.Title) %></a></span><strong></strong></li>
    <%}%>
	</ul>
</div>
<div class="tabs_b"></div>