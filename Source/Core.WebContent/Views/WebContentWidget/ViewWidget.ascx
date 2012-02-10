<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.NHibernate.Models.Article>" %>
<%@ Import Namespace="Core.WebContent.NHibernate.Models" %>
<%@ Import Namespace="Core.WebContent.NHibernate.Static" %>
<div class = "content-details">
    <%if (Model.Category.Section.SectionSettings.ShowAuthor == SectionSettingsVisibility.Both) {%>
        <h3><%=((ArticleLocale)Model.CurrentLocale).Title %></h3>
    <%} %>
     <%if (Model.Category.Section.SectionSettings.ShowAuthor == SectionSettingsVisibility.Both) {%>
        <h4 class = "summary"><%=((ArticleLocale)Model.CurrentLocale).Summary %></h4>
    <%} %>
    <%if (Model.Category.Section.SectionSettings.ShowAuthor == SectionSettingsVisibility.Both) {%>
        <h4 class = "summary"><%=Model%></h4>
    <%} %>
    <%if (Model.Category.Section.SectionSettings.ShowAuthor == SectionSettingsVisibility.Both) {%>
        <h4 class = "summary"><%=((ArticleLocale)Model.CurrentLocale).Description %></h4>
    <%} %>
</div>
