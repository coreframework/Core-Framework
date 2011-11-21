<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.WidgetDetailsModel>" %>
<%@ Assembly Name="Core.WebContent" %>
<%@ Assembly Name="Core.WebContent.NHibernate" %>
<%@ Import Namespace="Core.WebContent.NHibernate.Models" %>

<div class = "web-content-details">
    <%if (Model.ShowTitle) {%>
         <%if (Model.TitleLinkable)
           {%>
            <h2><a href="<%=Model.Article.Url%>"><%=((ArticleLocale)Model.Article.CurrentLocale).Title%></a></h2>
         <%} else{ %>
             <h2><%=((ArticleLocale)Model.Article.CurrentLocale).Title%></h2>
         <%}%>
    <%}%>
    <%if (Model.ShowCreatedDate) {%>
       <span class="add-info">
        <%=Model.Article.CreateDate.ToLongDateString()%>
       </span>
    <%} %>
    <%if (Model.ShowAuthor && !String.IsNullOrEmpty(Model.Article.Author)) {%>
       <span class="add-info">
        Added by <%=Model.Article.Author%>
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
    <%if (Model.ShowSummaryText) {%>
       <p class="summary"><%=((ArticleLocale)Model.Article.CurrentLocale).Summary %></p>
    <%}%>
    <%if (Model.ShowContent) {%>
       <%=((ArticleLocale)Model.Article.CurrentLocale).Description %>
    <%}%>
   <%if (Model.ShowDownloadLink && Model.Article.Files.Count() > 0) {%>
      <div class = "files">
           <%foreach (var file in Model.Article.Files) { %>
            <a class="file-info" href="<%=file.FileName%>"><%=file.Title%></a>
           <% } %>
      </div>
    <%}%>
    <%if (Model.ShowModifiedDate && Model.Article.LastModifiedDate != null) {%>
       <span class = "add-info">
            <%:Html.LocalizedLabelFor(model=>model.Article.LastModifiedDate)%>: <%=((DateTime)Model.Article.LastModifiedDate).ToLongDateString()%>
       </span>
    <%}%>
</div>
