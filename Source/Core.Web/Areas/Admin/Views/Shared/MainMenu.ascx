<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%=Html.RenderMenu(Url, Context) %>

<script type="text/javascript">
    $(function () {
        $("#accordion").accordion({ active: parseInt($('#active').attr('number')) });
    });
</script>