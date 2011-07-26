<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.NHibernate.Models.FormBuilderWidget>" %>
<%@ Import Namespace="Core.Forms.Extensions" %>
<%@ Import Namespace="Core.Forms.NHibernate.Models" %>
<div id="<%=String.Format("formHolder{0}", Model.Id)%>" class="widget-form">
    <% using (Ajax.BeginForm(
                        "SubmitWidgetForm",
                        "FormsBuilderWidget",
                        new { area = "Forms" },
                        new AjaxOptions
                            {
                                UpdateTargetId = String.Format("formHolder{0}", Model.Id)
                            }))
       {%>
    <input type="hidden" id="instanceId" name="instanceId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <h3><%:Model.Title%></h3>
    <%:Html.Messages() %>
    <% foreach (FormElement item in Model.Form.FormElements.OrderBy(el => el.OrderNumber))
    {%>
        <div class="form_i">
            <%=Html.FormElementRenderer(item, ViewData[String.Format("FormCollection{0}", Model.Id)] as FormCollection)%>
        </div>
    <%}%>

    <%if (Model.Form.ShowSubmitButton){%>
        <%:Html.Submit(((FormLocale)Model.Form.CurrentLocale).SubmitButtonText)%>
    <%}%>
    <%if (Model.Form.ShowResetButton){%>
       <input type="reset" value="<%=((FormLocale)Model.Form.CurrentLocale).ResetButtonText %>" />
    <%}%>

    <%:Html.AntiForgeryToken()%>
    <%}%>
</div>
