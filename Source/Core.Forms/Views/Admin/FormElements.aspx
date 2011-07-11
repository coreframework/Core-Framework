<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>
<%@ Import Namespace="System.Web.Mvc" %>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>Form Elements</h1>
</asp:Content>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
   <%= Html.JavascriptInclude("jquery-ui/jquery-ui-1.8.11.custom.min.js")%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em><input id="New" type="button" class="button" value="Add new element" /><strong></strong></div>
		</div>
    </div>
    <script type="text/javascript">
        $(function () { $('#New').click(function () { window.location = "<%: Url.Action("NewElement","Forms") %>"; }); });
    </script>

<%--     <div class="outset">
        <table class="index">
            <thead>
            <tr>
                <th>Title</th>
                <th>Type</th>
                <th>Required</th>
                <th>Actions</th>
            </tr>
            </thead>
            <tbody>

            <% foreach (var formElement in Model){ %>
            <tr>
                <td>
                    <%=Html.Hidden("formElementId", formElement.Id)%>
                    <%:formElement.Title%>
                </td>
                <td>
                  <%:formElement.Type%>
                </td>
                 <td>
                  <%:formElement.IsRequired%>
                </td>
                 <td>
                   <%:Html.RouteLink(Html.Translate(".Edit"), new { controller = "Forms", action = "EditElement", formElementId = formElement.Id, formId = formElement.Form.Id })%><br/>
                   <%:Html.RouteLink(Html.Translate(".Delete"), new { controller = "Forms", action = "Edit", formElementId = formElement.Id, formId = formElement.Form.Id })%><br/>
                </td>
            </tr>
            <% } %>
            </tbody>
        </table>
    </div>
   <div id="actions">
    <ul>
      <li>
         <%:Html.RouteLink("Add new element", new { controller = "Forms", action = "NewElement" })%>
      </li>
    </ul>
  </div>--%>

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
                        postData.formElementId = $('#formElementId', curItem).val();
                        postData.orderNumber = currentOrderNumber;
                        $.ajax({
                            url: '<%=Url.Action(FormsMVC.Forms.UpdateFormElementPosition()) %>',
                            type: 'POST',
                            data: postData
                        });
                    }
                }
            ).disableSelection();;
          
        });
    </script>
</asp:Content>
