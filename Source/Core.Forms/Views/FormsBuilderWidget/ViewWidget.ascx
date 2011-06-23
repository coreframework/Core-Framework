<%@ Assembly Name="Core.Forms" %>
<%@ Assembly Name="Core.Forms.NHibernate" %>
<%@ Import Namespace="Core.Forms.Models" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Forms.NHibernate.Models.FormBuilderWidget>" %>
<%@ Import Namespace="System.Web.Mvc.Ajax" %>
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
               <%: Html.ValidationSummary(true) %>
               <input type="hidden" id="instanceId" name="instanceId" value="<%= Html.Encode(Model.Id) %>" />
               <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
               <fieldset>
                 <h1><%:Model.Title%></h1>
                 <% foreach (FormElement item in Model.Form.FormElements.OrderBy(el=>el.OrderNumber))
                  {%>
                    <div  class="form-element">
                        <%=Html.FormElementRenderer(item, ViewData[String.Format("FormCollection{0}", Model.Id)] as FormCollection)%>
                   </div>
                 <%}%>
                 <div style="clear:both"></div>
                 <%: Html.Submit(Html.Translate(".Submit"))%>
             </fieldset>
            <%:Html.AntiForgeryToken()%>
       <%}%>
</div>

