<%@ Assembly Name="Core.Languages.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Language>>" %>
<div class="list-menu-widget horizontal">
    <%foreach (var language in Model)
      {%>
    <a class="change-lang" cult="<%=language.Culture %>" href="javascript:void(0);">
        <%= Html.Encode(language.Title)%></a>
    <% }%>
</div>
<script type="text/javascript">
    $(function () {
        $('.change-lang').click(function () {
            $.ajax({
                type: "POST",
                url: "<%=Url.Action("ChangeLanguage", "LanguageSelectorWidget") %>",
                data: { cultureCode: $(this).attr('cult') },
                success: function (response) {
                    location.reload();
                }
            });
        });
    });
</script>
