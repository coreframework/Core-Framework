<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Framework.MVC.Grids.jqGrid" %>
<%@ Import Namespace="Core.Forms.Models" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
   <%= Html.JavascriptInclude("jquery-ui/jquery-ui-1.8.11.custom.min.js")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1>Form Elements</h1>
   <%Html.RenderAction(FormsMVC.Forms.FormTabs((ViewData["Form"] is FormViewModel)?(ViewData["Form"] as FormViewModel).Id:0, false, true, false));%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
                     <% using (Html.BeginForm(FormsMVC.Forms.RemoveElement(formElement.Id), FormMethod.Post))
                    { %>
                        <%: Html.LinkSubmitButton(Html.Translate(".Delete"))%>
                      
                   <% } %>
                </td>
            </tr>
            <% } %>
            </tbody>
        </table>
    </div>--%>
<%--
   <div id="actions">
    <ul>
      <li>
         <%:Html.RouteLink("Add new element", new { controller = "Forms", action = "NewElement" })%>
      </li>
    </ul>
  </div>--%>
    <div class="e_table_area">
        <%=Html.JqGrid(model => model.SearchString) %>
        <%if (ViewData["Form"] is FormViewModel && ((FormViewModel)ViewData["Form"]).AllowManage)  {%>
		<div class="e_table_bottom clrfix">
			<div class="btn1 clrfix"><em></em>
            <input id="New" type="button" class="button" value="Add New Element" />
            <strong></strong></div>
		</div>
        <script type="text/javascript">
            $(function () { $('#New').click(function () { window.location = "<%:Url.Action("NewElement", "Forms")%>"; }); });
        </script>
        <% }%>
    </div>
    <script type="text/javascript">
        jQuery(function () { $('#New').click(function () { window.location = "<%: Url.Action("NewElement","Forms",new { formId = (Int64)ViewData["formId"]}) %>"; }); });
    </script>
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
