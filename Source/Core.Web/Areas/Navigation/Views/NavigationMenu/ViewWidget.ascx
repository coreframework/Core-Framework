<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Areas.Navigation.Models.PartialNavigationMenuModel>" %>
<%@ Import Namespace="Core.Web.Helpers.HtmlExtensions.MenuTreeView" %>

<%if (Model.Orientation == Core.Web.NHibernate.Models.Static.Orientation.Vertical)
  {%>
 <div class="vertical"><%
  }%>
    <div class="pages clrfix">
        <div class="pages-menu" id="pagesNavigationMenu<%=Model.WidgetId%>">
            <%=Html.RenderTree(Model.MenuItems, "clrfix", model=>Html.Partial(MVC.Navigation.NavigationMenu.Views.PartialNavigationMenuItem, model).ToString())%>
        </div>
    </div>

    <%if (Model.Orientation == Core.Web.NHibernate.Models.Static.Orientation.Vertical)
  {%></div><%}%>
 <div class="clear"></div>
 <script type="text/javascript" language="javascript">
     jQuery(function () {
         ddsmoothmenu.init({
             mainmenuid: "pagesNavigationMenu<%=Model.WidgetId %>", //menu DIV id
             orientation: <%if(Model.Orientation == Core.Web.NHibernate.Models.Static.Orientation.Horizontal)
                            {%>'h'<% }
                            else {%> 'v'<% }%>, //Horizontal or vertical menu: Set to "h" or "v"
             classname: 'pages-menu', //class added to menu's outer DIV
             contentsource: "markup",
             arrowimages: { down: ['downarrowclass', '<%:Links.Content.Images.ico_open_png %>', 17], right: ['rightarrowclass', '<%:Links.Content.Images.ico_right_png %>'] }
     
         });
     });
</script> 
