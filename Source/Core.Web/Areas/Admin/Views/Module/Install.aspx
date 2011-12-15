<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Admin/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Core.Web.Areas.Admin.Models.PluginViewModel>" %>

<asp:Content runat="server" ID="Content" ContentPlaceHolderID="TitleContent">
    <%: String.Format(Html.Translate(".Title"), Model) %></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="PageTitleContent" runat="server">
    <h1>
        <%: String.Format(Html.Translate(".Title"), Model.Title) %></h1>
</asp:Content>
<asp:Content runat="server" ID="Content2" ContentPlaceHolderID="MainContent">
    <%if (Model.MissingDependencies.Count == 0)
      {%>
    <p>
        <%:String.Format(Html.Translate(".AreYouSure"), Model.Title)%></p>
    <%
          using (Html.BeginForm(MVC.Admin.Module.ConfirmInstall(Model.Id), FormMethod.Post))
          {%>
    <div class="i_form clrfix">
        <div class="i_buttons clrfix">
            <div class="btn1 clrfix">
                <em></em>
                <%:Html.Submit(Html.Translate("Actions.Install"), new { @class = "button" })%>
                <strong></strong>
            </div>
            <span>
                <%:Html.ActionLink(Html.Translate("Actions.Cancel"), MVC.Admin.Module.Index())%></span>
        </div>
    </div>
    <%
        }%>
    <%
      }
      else
      {%>
    <p>
        <%:String.Format(Html.Translate(".MissingDependencies"), Model.Title)%></p>
    <ul>
        <%foreach (var missingDependency in Model.MissingDependencies)
          {%>
        <li>
            <%if (!String.IsNullOrEmpty(missingDependency.MinVersion) || !String.IsNullOrEmpty(missingDependency.MaxVersion))
              {%>
            <%:String.Format("{0}, v.{1}-{2}", missingDependency.Identifier, missingDependency.MinVersion,
                                    missingDependency.MaxVersion)%>
            <%
                }
              else
              {%>
            <%:String.Format("{0}", missingDependency.Identifier)%>
            <%
}%></li>
        <%
          }%>
    </ul>
    <%
      }%>
</asp:Content>
