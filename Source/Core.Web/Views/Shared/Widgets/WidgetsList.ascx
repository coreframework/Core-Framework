<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.NHibernate.Models.Widget>>" %>
<div>
    Click on any of the item to add it to your page
    <br/>
    <br/>
    <ul class="widgets">
        <%foreach (var item in Model) {%>
           <li>
                <%=Ajax.ActionLink(Html.Encode(item.Title), MVC.Pages.AddWidget((long)ViewData["pageId"], item.Identifier), new AjaxOptions { OnSuccess = "addWidget", OnComplete = "addLinkCss" }, new { @widgetID = item.Plugin.Identifier })%>
           </li>
        <%} %>
    </ul>
</div>
<script type="text/javascript">
    function addWidget(content) {
        $('#tblLayoutHolder td.column').first().prepend(content.get_data());
        iNettutsInit();
     }
     function addLinkCss(content) {
         var uid = $(this).attr('widgetID');
         $('<link type="text/css" rel="stylesheet" media="screen, projection" href="<%= HttpContext.Current.Request.ApplicationPath %>/styles.cssx?package=' + uid + '" />').appendTo($('head'));
     }
</script>
