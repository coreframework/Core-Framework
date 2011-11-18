<%@ Assembly Name="Core.WebContent" %>
<%@ Assembly Name="Core.WebContent.NHibernate" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.WebContent.Models.WebContentWidgetViewModel>" %>

<div class="form_area">
    <%: Html.ValidationSummary(true) %>
    <input type="hidden" id="widgetId" name="widgetId" value="<%= Html.Encode(Model.Id) %>" />
    <input type="hidden" id="Id" name="Id" value="<%= Html.Encode(Model.Id) %>" />
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.SectionId)%>
        <%:Html.DropDownListFor(model => model.SectionId, new SelectList(Model.Sections, "Id", "CurrentLocale.Title", Model.SectionId), "Please select", new { })%>
        <%:Html.ValidationMessageFor(model=>model.SectionId) %>
    </div>
     <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.CategoriesId)%>
        <%:Html.DropDownListFor(model => model.CategoriesId, new {multiple="multiple"})%>
        <%:Html.ValidationMessageFor(model => model.CategoriesId)%>
    </div>
     <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.ArticleId)%>
        <%:Html.DropDownListFor(model => model.ArticleId)%>
        <%:Html.ValidationMessageFor(model => model.ArticleId)%>
    </div>
    <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.ViewMode)%><br/>
        <%:Html.DropDownListFor(model => model.ViewMode)%>
        <%:Html.ValidationMessageFor(model => model.ViewMode)%><br/>
    </div>
     <div class="form_i">
        <%:Html.CheckBoxFor(model => model.ShowPagination)%>
        <%:Html.LocalizedLabelFor(model => model.ShowPagination, new { Class = "checkbx-label" })%>
     </div>
     <div class="form_i">
        <%:Html.LocalizedLabelFor(model => model.ItemsNumber)%><br/>
        <%:Html.TextBoxFor(model => model.ItemsNumber, new { Class = "inp_txt" })%>
        <%:Html.ValidationMessageFor(model => model.ItemsNumber)%>
     </div>
    <%:Html.AntiForgeryToken()%>
</div>
 <script type="text/javascript">
     $(document).ready(function () {
        $("#CategoriesId").CascadingDropDown("#SectionId",
            '<%: Url.Action("LoadCategories", "WebContentWidget")%>', { showPromtText: false,
            postData: function () {
                return { categoriesId: '<%=Model.CategoriesId!=null?String.Join(",", Model.CategoriesId):String.Empty%>', sectionId: $('#SectionId').val() 
               };
            },
            onLoaded: function () {
                $("#CategoriesId").multiselect('refresh');
                $("#CategoriesId").multiselect('enable');
            },
            onReset: function () {
                $("#CategoriesId").multiselect('refresh');
                $("#CategoriesId").multiselect('disable');
            }
        });

          $("#ArticleId").CascadingDropDown("#CategoriesId",
            '<%: Url.Action("LoadArticles", "WebContentWidget")%>', { 
            postData: function () {
            var categories = $('#CategoriesId').val() || [];
                return { articleId: <%=Model.ArticleId ?? 0%>, categoriesId: categories.join(",") 
            };
        } 
        });
        $("#CategoriesId").multiselect({selectedList: 4});
     });
</script>

