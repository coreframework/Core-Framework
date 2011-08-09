<%@ Assembly Name="Core.News" %>
<%@ Assembly Name="Core.News.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsArticleWidgetModel>" %>
<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i"> 
        <label><%=Html.Translate(".Categories") %></label><br/>
        <%:Html.ListBoxFor(model => model.CategoriesId, new MultiSelectList(Model.Categories, "Id", "Title", Model.CategoriesId), new { style="height:140px" })%>
        <%:Html.ValidationMessageFor(model => model.CategoriesId)%>
    </div>
    <div class="form_i">
       <label><%=Html.Translate(".Count") %></label><br/>
        <%:Html.TextBoxFor(model => model.ItemsOnPage)%>
        <%:Html.ValidationMessageFor(model => model.ItemsOnPage)%>        
    </div>
    <div class="form_i">
       <label><%=Html.Translate(".ShowPaginator") %></label><br/>
        <%:Html.CheckBoxFor(model => model.ShowPaginator)%>
        <%:Html.ValidationMessageFor(model => model.ItemsOnPage)%>        
    </div>
    <%:Html.AntiForgeryToken()%>
</div>
