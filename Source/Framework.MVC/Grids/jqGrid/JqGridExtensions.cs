// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JqGridExtensions.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Framework.MVC.Extensions;
using Framework.MVC.Helpers;

namespace Framework.MVC.Grids.jqGrid
{
    /// <summary>
    /// Extension class for HtmlHelper.
    /// </summary>
    public static class JqGridExtensions
    {
        #region Constants

        private const String ScriptTemplate =
            @"$(window).load(function () {{
                var grid = $('#list');
                var pager = $('#pager');
                var search = $('#search');
                grid.jqGrid({{
                    url: '{0}',
                    mtype: 'post',
                    datatype: 'json',
                    colModel: [{1}],
                    rowNum: 10,
                    rowList: [10, 20, 50],
                    pager: pager,
                    sortname: '{2}',
                    sortorder: '{3}',
                    viewrecords: true,
                    multiselect: {6},
                    width: '100%',
                    height: '100%',
                    autowidth: true,
                    rownumbers: true,
                    caption: '{4}',
                    loadComplete: function () {{
                       var selectedIDs = [{8}];
                       for(var id in selectedIDs) {{ 
                            grid.setSelection(selectedIDs[id].toString()); 
                       }}
                    }},
                    onSelectRow: function (id) {{
                        if(parseInt(id) && !{7}) {{
                            location.href = '{5}' + id;
                        }}
                    }}
                }}).navGrid(pager, {{ edit: false, add: false, del: false, search: false }});
                search.filterGrid('#' + grid.attr('id'), {{
                    gridModel: false,
                    filterModel: [{{
                        label: 'Search',
                        name: 'search',
                        stype: 'text'
                    }}]
                }});
            }});
            var timeoutHnd;
            var flAuto = false;
            function doSearch(ev) {{
                if (ev.keyCode == 13) {{
                    timeoutHnd = setTimeout(gridReload, 500);
                    return;
                }}
                if (!flAuto) return;
                // var elem = ev.target||ev.srcElement;
                if (timeoutHnd) {{
                    clearTimeout(timeoutHnd)
                }}
                if ((ev.keyCode >= 65 && ev.keyCode <= 90) || ev.keyCode == 8 || ev.keyCode == 46 || (ev.keyCode >= 48 && ev.keyCode <= 57) || (ev.keyCode >= 96 && ev.keyCode <= 105)) {{
                    timeoutHnd = setTimeout(gridReload, 500)
                }}
            }}
            function gridReload() {{
                var search = $('#SearchString').val();
                jQuery('#list').setGridParam({{ url: '{0}?search=' + search, page: 1 }}).trigger('reloadGrid');
            }}
            function enableAutosubmit(state) {{
                flAuto = state;
                jQuery('#submitButton').attr('disabled', state);
            }} ";

        #endregion

        /// <summary>
        /// Grids the specified HTML.
        /// </summary>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML helper.</param>
        /// <param name="searchExpression">The search expression.</param>
        /// <returns>Html-markup for jq grid.</returns>
        public static MvcHtmlString JqGrid<TValue>(this HtmlHelper<GridViewModel> html, Expression<Func<GridViewModel, TValue>> searchExpression)
        {
            var builder = new StringBuilder();
            var filterWrapper = new TagBuilder("div");
            filterWrapper.AddCssClass("e_table_top clrfix");

            var searchWrapper = new TagBuilder("div");
            searchWrapper.AddCssClass("filter_l clrfix");

            searchWrapper.InnerHtml += GenerateSearch(html, searchExpression);
            searchWrapper.InnerHtml += GenerateAutoSearch(html);
            searchWrapper.InnerHtml += GenerateSearchButtons(html);

            filterWrapper.InnerHtml += searchWrapper.ToString(TagRenderMode.Normal);
            
            builder.Append(filterWrapper.ToString(TagRenderMode.Normal));

            builder.Append(GenerateGrid());

            builder.Append(GeneratePager());
            
            builder.Append(GenerateScript(html));
            
            MvcHtmlString result = MvcHtmlString.Create(builder.ToString());

            return result;
        }

        private static String GenerateSearch<TValue>(HtmlHelper<GridViewModel> html, Expression<Func<GridViewModel, TValue>> searchExpression)
        {
            var searchWrapper = new TagBuilder("div");
            searchWrapper.AddCssClass("filter_i");
            var searchLabel = new TagBuilder("label");
            searchLabel.SetInnerText(String.Format("{0}:", html.Translate("Search", ResourceHelper.GetModelScope(typeof(GridViewModel)))));
            searchWrapper.InnerHtml += searchLabel.ToString(TagRenderMode.Normal);
            searchWrapper.InnerHtml += html.TextBoxFor(searchExpression, new { onkeydown = "doSearch(arguments[0]||event)" }).ToHtmlString();
            return searchWrapper.ToString(TagRenderMode.Normal);
        }

        private static String GenerateAutoSearch<TModel>(HtmlHelper<TModel> html)
        {
            var autoSearchWrapper = new TagBuilder("div");
            autoSearchWrapper.AddCssClass("filter_i");
            var autoSearchLabel = new TagBuilder("label");
            autoSearchLabel.SetInnerText(html.Translate("EnableAutosearch", ResourceHelper.GetModelScope(typeof(GridViewModel))));
            autoSearchWrapper.InnerHtml += autoSearchLabel.ToString(TagRenderMode.Normal);
            autoSearchWrapper.InnerHtml += html.CheckBox("autosearch", new { onclick = "enableAutosubmit(this.checked)", style = "width: auto; border: none;" }).ToHtmlString();

            return autoSearchWrapper.ToString(TagRenderMode.Normal);
        }

        private static String GenerateSearchButtons<TModel>(HtmlHelper<TModel> html)
        {
            var buttonsWrapper = new TagBuilder("div");
            buttonsWrapper.AddCssClass("btn2 clrfix");
            var em = new TagBuilder("em");
            var strong = new TagBuilder("strong");
            var button = new TagBuilder("input");
            button.AddCssClass("button");
            button.Attributes["type"] = "button";
            button.Attributes["value"] = html.Translate("Search", ResourceHelper.GetModelScope(typeof(GridViewModel)));
            button.GenerateId("submitButton");
            button.MergeAttribute("onclick", @"gridReload()");
            buttonsWrapper.InnerHtml += em.ToString(TagRenderMode.Normal);
            buttonsWrapper.InnerHtml += button.ToString(TagRenderMode.Normal);
            buttonsWrapper.InnerHtml += strong.ToString(TagRenderMode.Normal);

            return buttonsWrapper.ToString(TagRenderMode.Normal);
        }

        private static String GenerateGrid()
        {
            var gridTable = new TagBuilder("table");
            gridTable.GenerateId("list");
            gridTable.AddCssClass("scroll");
            gridTable.Attributes.Add("cellpadding", "0");
            gridTable.Attributes.Add("cellspacing", "0");

            return gridTable.ToString(TagRenderMode.Normal);
        }

        private static String GeneratePager()
        {
            var pagerWrapper = new TagBuilder("div");
            pagerWrapper.GenerateId("pager");
            pagerWrapper.AddCssClass("scroll");

            return pagerWrapper.ToString(TagRenderMode.Normal);
        }

        private static String GenerateScript(HtmlHelper<GridViewModel> html)
        {
            var model = html.ViewData.Model;
            var script = new TagBuilder("script");
            script.MergeAttribute("type", @"text/javascript");
            script.MergeAttribute("language", @"javascript");
            script.InnerHtml = String.Format(ScriptTemplate, model.DataUrl, GenerateColumnsBlockScript(model.Columns), model.DefaultOrderColumn, model.IsAsc ? "asc" : "desc", model.GridTitle, model.DetailsUrl, model.MultiSelect.ToString().ToLower(), model.IsRowNotClickable.ToString().ToLower(), GetJsArray(model.SelectedIds));

            return script.ToString(TagRenderMode.Normal);
        }

        private static String GenerateColumnsBlockScript(IEnumerable<GridColumnViewModel> columns)
        {
            var builder = new StringBuilder();
            foreach (var column in columns)
            {
                builder.AppendFormat(@"{{name: '{0}', index: '{1}', align: '{2}', resizable: {3},", column.Name, column.Index, column.Align, column.Resizable.ToString().ToLower());
                if (column.Width.HasValue)
                {
                    builder.AppendFormat(" width: {0},", column.Width.Value);
                }
                builder.AppendFormat(@" sortable: {0}, hidden: {1} }},", column.Sortable.ToString().ToLower(), column.Hidden.ToString().ToLower());
            }

            return builder.ToString();
        }

        private static String GetJsArray(IEnumerable<long> list)
        {
            var temp = new StringBuilder();
            foreach (var l in list)
            {
                temp.Append(l + ",");
            }
            if (temp.Length > 0)
            {
                temp = temp.Remove(temp.Length - 1, 1);
            }
            return temp.ToString();
        }
    }
}