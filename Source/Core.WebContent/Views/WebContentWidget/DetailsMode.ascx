<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.WidgetDetailsModel>" %>
<%@ Import Namespace="Core.WebContent.NHibernate.Models" %>

<div class = "content-details">
    <%if (Model.ShowTitle) {%>
        <h3><%=((ArticleLocale)Model.Article.CurrentLocale).Title %></h3>
    <%}%>
    <%if (Model.ShowSummaryText) {%>
       <p class="summary"><%=((ArticleLocale)Model.Article.CurrentLocale).Summary %></p>
    <%}%>
    <%if (Model.ShowCreatedDate) {%>
       <div class="add-info">
        <%=Model.Article.CreateDate.ToLongDateString()%>
       </div>
    <%} %>
    <%if (Model.ShowAuthor && String.IsNullOrEmpty(Model.Article.Author)) {%>
       <span class="add-info">
        <%=Model.Article.Author%>
       </span>
    <%} %>
    <%if (Model.ShowCategory) {%>
       <span class="add-info">
        Category: <%=((WebContentCategoryLocale)Model.Article.Category.CurrentLocale).Title%>
       </span>
    <%} %>
    <%if (Model.ShowSection) {%>
       <span class="add-info">
        Section: <%=((SectionLocale)Model.Article.Category.Section.CurrentLocale).Title%>
       </span>
    <%} %>
    <%if (Model.ShowContent) {%>
       <%=((ArticleLocale)Model.Article.CurrentLocale).Description %>
    <%}%>
   <%if (Model.ShowDownloadLink && Model.Article.Files.Count() > 0) {%>

       <%foreach (var file in Model.Article.Files) { %>
        <a href="<%=file.FileName%>"><%=file.Title%></a>
       <% } %>
    
    <%}%>
    <%if (Model.ShowModifiedDate && Model.Article.LastModifiedDate != null) {%>
       <span class = "add-info">
            <%:Html.LocalizedLabelFor(model=>model.Article.LastModifiedDate)%> <%=((DateTime)Model.Article.LastModifiedDate).ToLongDateString()%>
       </span>
    <%}%>
</div>
