<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.ArticleViewModel>" %>
<div>
    <div class="cols clrfix">
        <div class="fst_col colls_i">
            <%:Html.HiddenFor(model=>model.Id) %> 
            <%:Html.HiddenFor(model => model.SelectedCulture) %>
            <h3><%:Html.Translate("Common", "WebContent.Views.Article")%>:</h3>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.SectionId)%>
                <%:Html.DropDownListFor(model => model.SectionId, new SelectList(Model.Sections, "Id", "CurrentLocale.Title", Model.SectionId), "Please select", new { })%>
                <%:Html.ValidationMessageFor(model=>model.SectionId) %>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.CategoryId)%>
                <%:Html.DropDownListFor(model => model.CategoryId)%>
                <%:Html.ValidationMessageFor(model => model.CategoryId)%>
            </div>
             <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.Status)%>
                <%:Html.DropDownListFor(model => model.Status)%>
                <%:Html.ValidationMessageFor(model=>model.Status) %>
            </div>
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.Author)%>
            </div>         
            <h3><%:Html.Translate("Content", "WebContent.Views.Article")%>:</h3>
		    <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.Title) %>
                <%:Html.TextBoxFor(model => model.Title)%>
                <%:Html.ValidationMessageFor(model => model.Title) %>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model=>model.Summary) %>
                <%:Html.TextAreaFor(model => model.Summary)%>
                <%:Html.ValidationMessageFor(model => model.Summary)%>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.UrlType)%>
                <%:Html.DropDownListFor(model => model.UrlType)%>
                <%:Html.ValidationMessageFor(model => model.UrlType)%>
            </div>
            <div class="i_form_i">
                <%:Html.LocalizedLabelFor(model => model.Url)%>
                <%:Html.TextBoxFor(model => model.Url)%>
                <%:Html.ValidationMessageFor(model => model.Url)%>
            </div>
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.StartPublishingDate)%>
                <div class="clear"></div>
            </div> 
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.FinishPublishingDate)%>
                 <div class="clear"></div>
            </div> 
            <div class="i_form_i">
                <%:Html.EditorFor(model => model.Content)%>
            </div>
		    <div class="i_form_i">
            <%:Html.AntiForgeryToken()%>
            </div>
        </div>
    </div>
    <div class="i_buttons clrfix">
         <%if (Model.AllowManage){%>
		    <div class="btn1 clrfix">
                <em></em>
                <%:Html.Submit(Html.Translate("Actions.Save"), new {@class = "button"})%>
                <strong></strong>
            </div>
            <%if (Model.Id > 0) {%>
                <span><%:Html.ActionLink(Html.Translate("Actions.Remove"), WebContentMVC.Article.Remove(Model.Id))%></span>
            <%}%>
         <%}%>
	    <span><%:Html.RouteLink(Html.Translate("Actions.Cancel"), new { controller = "Article", action = "Show" })%></span>
    </div>
    <script type="text/javascript">
        $("#CategoryId").CascadingDropDown("#SectionId",
            '<%: Url.Action("SectionCategories", "Article")%>', { 
            postData: function () {
                return { categoryId: <%=Model.CategoryId%>, sectionId: $('#SectionId').val() 
            };
        } 
        });
    </script>
</div>
