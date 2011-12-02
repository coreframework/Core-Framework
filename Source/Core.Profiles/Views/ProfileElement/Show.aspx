<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Framework.Mvc.Grids.GridViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Framework.Mvc.Grids.JqGrid" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
   <%= Html.JavascriptInclude("jquery-ui/jquery-ui-1.8.11.custom.min.js")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>Profile Elements</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
        <div class="e_table_bottom clrfix">
            <div class="btn1 clrfix">
                <em></em>
                <input id="New" type="button" class="button" value="<%:Html.Translate("Actions.AddNewElement","Forms") %>" />
                <strong></strong>
            </div>
        </div>
        <script type="text/javascript">
            $(function () { $('#New').click(function () { window.location = "<%:Url.Action("New", "ProfileElement")%>"; }); });
        </script>
    </div>
    <script type="text/javascript">
        jQuery(function () {
            var fixHelper = function (e, ui) {
                ui.children().each(function () {
                    $(this).width($(this).width());
                });
                return ui;
            };
            $("#list").sortable(
                {
                    helper: fixHelper,
                    items: "tr",
                    stop: function (e, ui) {
                        var curItem = $(ui.item);
                        var currentOrderNumber = curItem.parent().children().index(curItem) + 1;
                        var postData = {};
                        postData.profileElementId = $('#profileElementId', curItem).val();
                        postData.orderNumber = currentOrderNumber;
                        $.ajax({
                            url: '<%=Url.Action("UpdateProfileElementPosition", "ProfileElement") %>',
                            type: 'POST',
                            data: postData
                        });
                    }
                }
            ).disableSelection(); ;

        });
    </script>
</asp:Content>

