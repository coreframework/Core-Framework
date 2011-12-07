<%@ Assembly Name="Core.Profiles" %>
<%@ Assembly Name="Core.Profiles.NHibernate" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<System.Collections.Generic.List<Core.Profiles.NHibernate.Models.ProfileHeader>>" %>
<%@ Import Namespace="System.Web.Mvc" %>
<%@ Import Namespace="Framework.Mvc.Grids" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="HeadContent">
   <%= Html.JavascriptInclude("jquery-ui/jquery-ui-1.8.11.custom.min.js")%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
   <%:Html.Translate("ProfileElements", "Profiles.Views.ProfileType")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageTitleContent" runat="server">
  <h1><%:Html.Translate("ProfileElements", "Profiles.Views.ProfileType")%></h1>
  <div class="tabs clrfix">
	<ul class="i-tab clrfix">
        <li>
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Details", "Profiles.Views.ProfileType"), "Edit", "ProfileType")%>
            </span>
            <strong></strong>
        </li>
        <li class="active">
            <em></em>
            <span>
             <%:Html.ActionLink(Html.Translate("Elements", "Profiles.Views.ProfileType"), "Show", "ProfileElement")%>
            </span>
            <strong></strong>
        </li>
	</ul>
  </div>
  <div class="tabs_b"></div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="e_table_area">
    <table class="e_table">
        <tbody>
            <tr>
                <th class="row"><span></span><%: Html.Translate("Title", "Profiles.Views.ProfileType")%></th>
            </tr>
        </tbody>
     </table>
    <div id="sortable-area">
    <%if (Model.Count > 0)
      {%>
       <ul class="grouping">
            <%
        foreach (var header in Model)
        {%>
                <li class ="profile-header">
                    <span>
                        <a href="<%:Url.Action("Edit", "ProfileHeader", new {profileHeaderId = header.Id})%>">
                            <%:header.Title%>
                        </a>
                    </span>
                    <%
        using (Html.BeginForm("Remove", "ProfileHeader", FormMethod.Post))
        {%>
                        <a class="delete"><em style="margin-left: 10px;" class="delete"></em></a>
                         <%:Html.Hidden("profileHeaderId", header.Id)%>
                    <%
        }%>
                    <ul <%:header.ProfileElements.Count == 0 ? "class=no-items" : String.Empty%>>
                        <%
        foreach (var element in header.ProfileElements.OrderBy(el => el.OrderNumber))
        {%>
                            <li class="profile-element">
                                <span>
                                     <a href="<%:Url.Action("Edit", "ProfileElement", new {profileElementId = element.Id})%>">
                                        <%:element.Title%>
                                    </a>
                                </span>
                                 <%
        using (Html.BeginForm("Remove", "ProfileElement", FormMethod.Post))
        {%>
                                    <a class="delete"><em style="margin-left: 10px;" class="delete"></em></a>
                                    <%:Html.Hidden("profileElementId", element.Id)%>
                                <%
        }%>
                            </li>
                        <%
        }%>
                    </ul>
                </li>
           <%
        }%>
        </ul>
        <%
        }
      else
      {%>
         <div class="noItemToDisplay"><%:Html.Translate("NoItemToDisplay", ResourceHelper.GetModelScope(typeof(GridViewModel)))%></div>
      <% } %>
    </div>
    <div class="e_table_bottom clrfix">
        <div class="btn1 clrfix">
            <em></em>
            <input id="NewElement" type="button" class="button" value="<%:Html.Translate("Actions.AddNewElement","Profiles") %>" />
            <strong></strong>
        </div>
         <div class="btn1 clrfix">
            <em></em>
            <input id="NewHeader" type="button" class="button" value="<%:Html.Translate("Actions.AddHeader","Profiles") %>" />
            <strong></strong>
        </div>
    </div>
    <script type="text/javascript">
        $(function () { 
            $('#NewElement').click(function () { window.location = "<%:Url.Action("New", "ProfileElement")%>"; }); 
            $('#NewHeader').click(function () { window.location = "<%:Url.Action("New", "ProfileHeader")%>"; }); 
        });
    </script>
</div>
<script type="text/javascript">
    jQuery(function () {
        $('.e_table_area a.delete').click(function () {
            $(this).closest('form')[0].submit();
        });

        $("#sortable-area ul.grouping, #sortable-area ul.grouping ul").sortable(
            {
                items: "li",
                stop: function (e, ui) {
                    var curItem = $(ui.item);
                    var currentOrderNumber = curItem.parent().children().index(curItem) + 1;
                    var postData = {};
                    if (curItem.hasClass("profile-header")) {
                        postData.profileHeaderId = $('#profileHeaderId', curItem).val();
                    }
                    if (curItem.hasClass("profile-element")) {
                        postData.profileElementId = $('#profileElementId', curItem).val();
                    }
                    postData.orderNumber = currentOrderNumber;
                    $.ajax({
                        url: '<%=Url.Action("UpdateProfileElementPosition", "ProfileElement") %>',
                        type: 'POST',
                        data: postData
                    });
                }
            }
        );
            $("#sortable-area ul.grouping, #sortable-area ul.grouping ul").disableSelection();
    });
</script>
</asp:Content>

