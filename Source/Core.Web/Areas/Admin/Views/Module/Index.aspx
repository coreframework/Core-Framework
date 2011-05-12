<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.IEnumerable<Core.Web.Areas.Admin.Models.PluginListModel>>" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Registered modules
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="outset">
        <table class="index">
            <thead>
                <tr>
                    <th>
                        Module Name
                        <br />
                        Description
                    </th>
                    <th>
                        Version
                    </th>
                    <th>
                        Added
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
                <%foreach (var corePlugin in Model)
                  {%>
                <tr class="node level_1">
                    <td>
                        <%=corePlugin.Title %><br />
                        <%=corePlugin.Description %><br />
                    </td>
                    <td>
                        <%=corePlugin.Version %>
                    </td>
                    <td>
                        <%=corePlugin.CreateDate %>
                    </td>
                    <td>
                        <%=corePlugin.Status %>
                    </td>
                    <td class="action">
                        <%if (corePlugin.Status.Equals(PluginStatus.NotInstalled))
                          {%>
                        <%: Html.ActionLink(Html.Translate(".Install"), MVC.Admin.Module.Install(corePlugin.Id)) %>
                        <%}
                          else if (corePlugin.Status.Equals(PluginStatus.Installed))
                          {%>
                        <%: Html.ActionLink(Html.Translate(".Uninstall"), MVC.Admin.Module.Uninstall(corePlugin.Id)) %>
                        <%
                            }%>
                    </td>
                </tr>
                <% } %>
            </tbody>
        </table>
    </div>
</asp:Content>
