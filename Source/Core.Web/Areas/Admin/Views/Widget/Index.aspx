<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Core.Web.NHibernate.Models.Widget>>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Registered widgets
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="outset">
        <table class="index">
            <thead>
                <tr>
                    <th>
                       Widgets
                    </th>
                    <th>
                        Plugin
                    </th>
                    <th>
                        Status
                    </th>
                    <th class="actions">
                        Actions
                    </th>
                </tr>
            </thead>
            <tbody>
                <%foreach (var widget in Model)
                  {%>
                <tr class="node level_1">
                    <td>
                        <%=widget.Title%>
                    </td>
                    <td>
                         <%=widget.Plugin.Title%>
                    </td>
                    <td>
                        <%=widget.Status%>
                    </td>
                    <td class="action">
                        <%if (widget.Status.Equals(WidgetStatus.Enabled))
                          {%>
                             <% using (Html.BeginForm(MVC.Admin.Widget.Disable(widget.Id), FormMethod.Post))
                                { %>
                                    <%: Html.LinkSubmitButton("Disable")%>
                      
                               <% } %>
                        <%}
                          else if (widget.Status.Equals(WidgetStatus.Disabled))
                          {%>
                             <% using (Html.BeginForm(MVC.Admin.Widget.Enable(widget.Id), FormMethod.Post))
                                { %>
                                    <%: Html.LinkSubmitButton("Enable")%>
                      
                               <% } %>
                        <%}%>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
</asp:Content>