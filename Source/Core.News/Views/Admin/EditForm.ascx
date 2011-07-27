<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.News.Models.NewsArticleLocaleViewModel>" %>
<% using (Html.BeginForm("Edit", "News", new { id = Model.NewsArticleId }))
   {%>
<div class="cols clrfix">
    <div class="fst_col colls_i" style="width: 60%;">
        <%:Html.HiddenFor(model => model.SelectedCulture) %>
        <div class="i_form_i">
            <%:Html.EditorFor(model => model.Title)%>
        </div>
    <div class="i_form_i">
        <%:Html.EditorFor(model => model.Content)%>
    </div>
    <div class="i_form_i">
        <%:Html.AntiForgeryToken()%>
    </div>
    </div>
</div>
<div class="i_buttons clrfix">
    <div class="btn1 clrfix">
        <em></em>
        <%: Html.Submit(String.Format(Html.Translate(".Save"), Model),new { @class="button"})%>
        <strong></strong>
    </div>
    <span>
        <%:Html.RouteLink(String.Format(Html.Translate(".Cancel"), Model), new { controller = "News", action = "ShowAll"})%></span>
</div>
<% }%>