<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.LoginWorkflow.Models.LoginHolderWidgetEditModel>" %>
<%@ Assembly Name="Core.FormLogin" %>
<%@ Assembly Name="Core.OpenIDLogin" %>
<%@ Assembly Name="Core.LoginWorkflow" %>
<%@ Import Namespace="Core.FormLogin" %>
<%@ Import Namespace="Core.OpenIDLogin" %>
<%@ Import Namespace="Framework.Mvc.Extensions" %>
<%:Html.ValidationSummary(true)%>
<%:Html.Messages()%>
<input type="hidden" id="widgetId" name="widgetId" value="<%=Html.Encode(Model.Id)%>" />
<input type="hidden" id="Id" name="Id" value="<%=Html.Encode(Model.Id)%>" />
<b>Login Widget:</b>
<% Html.RenderPartial(String.Format("~/Areas/{0}/{1}", FormLoginPlugin.Instance.PluginAreaDirectoryName, FormLoginMVC.LoginWidget.Views.EditWidget.Replace("~/", String.Empty)), Model.LoginWidgetEditModel); %>
<b>OpenID Widget:</b>
<% Html.RenderPartial(String.Format("~/Areas/{0}/{1}", OpenIDLoginPlugin.Instance.PluginAreaDirectoryName, OpenIDLoginMVC.OpenIDLoginWidget.Views.EditWidget.Replace("~/", String.Empty)), Model.OpenIDLoginWidgetEditModel); %>
