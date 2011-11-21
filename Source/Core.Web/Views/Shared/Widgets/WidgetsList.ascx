<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.Collections.Generic.IEnumerable<Core.Web.NHibernate.Models.Widget>>" %>
<%@ Import Namespace="Framework.Core.Infrastructure" %>
<%@ Import Namespace="Framework.Core" %>

<%:Html.Message(MessageType.Info, Html.Translate(".AddWidget")) %>
<div class="form_area">
    <ul class="widgets">
        <%foreach (var item in Model){
              %>
           <li>
                <%=Ajax.ActionLink(Html.Encode(item.Title), MVC.Pages.AddWidget((long)ViewData["pageId"], item.Id), new AjaxOptions { OnSuccess = "addWidget" }, new { @pluginID = item.Plugin != null ? item.Plugin.Identifier : String.Empty })%>
           </li>
        <%} %>
    </ul>
</div>
<script type="text/javascript">
    function addWidget(content) {
        addJsCss(this);
        var contentData = content.get_data();
        $('#tblLayoutHolder table td.column').first().prepend(contentData);
        iNettutsInit($stickyFooter);
        $stickyFooter.positionFooter();
        $('.widget_title a.edit').unbind('click').click(function() {editWidgetClicked(this, '<%=Url.Action(MVC.Pages.ShowSettings())%>?pageWidgetId=', '.widget');});        
     }
     function addJsCss(link) {
         var uid = $(link).attr('pluginID');
         if(uid) {

         //load css
         var cssInclude = '<%= ApplicationUtility.Path %>styles.cssx?package=' + uid;
         loadjscssfile(cssInclude, "css");

         //load js file
         var jsInclude = '<%= ApplicationUtility.Path %>scripts.jsx?pageId=' + <%=(long)ViewData["pageId"]%>+'&id='+uid;
         loadjscssfile(jsInclude, "js");
         }
     }
</script>
