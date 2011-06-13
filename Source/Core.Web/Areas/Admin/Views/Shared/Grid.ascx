<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Framework.MVC.Grids.GridViewModel>" %>
<%@ Import Namespace="Core.Web.Areas.Admin.Models" %>
<%@ Import Namespace="Framework.MVC.Grids" %>
<div style="margin-top: 20px; margin-left: 20px;">
    <div class="form_area">
        <label>
            Search</label>
        <%: Html.TextBoxFor(model => model.SearchString, new { onkeydown = "doSearch(arguments[0]||event)" })%>
        <div id="search" style="visibility: hidden; width: 10px; height: 10px">
        </div>
    </div>
    <div class="form_area">
        <label for="autosearch">
            <%:Html.Translate(".EnableAutosearch", ResourceHelper.GetModelScope(typeof(GridViewModel)))%></label>
        <input type="checkbox" id="autosearch" onclick="enableAutosubmit(this.checked)" style="width: auto;" />
    </div>
    <p class="buttons">
        <button onclick="gridReload()" id="submitButton">
            <%:Html.Translate(".Search", ResourceHelper.GetModelScope(typeof(GridViewModel)))%></button>
    </p>
</div>
<table id="list" class="scroll" cellpadding="0" cellspacing="0">
</table>
<div id="pager" class="scroll" style="text-align: center;">
</div>
<script type="text/javascript" language="javascript">
    $(window).load(function () {
        var grid = $("#list");
        var pager = $("#pager");
        var search = $("#search");
        grid.jqGrid({
            url: '<%=Model.DataUrl %>',
            mtype: "post",
            datatype: "json",
            colModel: [
                <%foreach (var column in Model.Columns)
                {%>
                    {name: '<%=column.Name %>', index: '<%=column.Index %>', align: '<%=column.Align %>', resizable: <%=column.Resizable.ToString().ToLower() %>,
                    <%if(column.Width.HasValue)
                    {%>     width: <%=column.Width.Value %>,
                    <%}%> sortable: <%=column.Sortable.ToString().ToLower() %>, hidden: <%=column.Hidden.ToString().ToLower() %>
                    },
                <%} %>
                  ],
            rowNum: 10,
            rowList: [10, 20, 50],
            pager: pager,
            sortname: '<%=Model.DefaultOrderColumn %>',
            sortorder: '<%=Model.IsAsc ? "asc" : "desc"%>',
            viewrecords: true,
            multiselect: false,
            width: '100%',
            height: '100%',
            autowidth: true,
            rownumbers: true,
            caption: '<%=Model.GridTitle %>',
            onSelectRow: function (id) {
                if(parseInt(id)) {
                    location.href = '<%=Model.DetailsUrl %>' + id;
                }
            }
        }).navGrid(pager, { edit: false, add: false, del: false, search: false });
        search.filterGrid("#" + grid.attr("id"), {
            gridModel: false,
            filterModel: [{
                label: 'Search',
                name: 'search',
                stype: 'text'
            }]
        });
    });

    var timeoutHnd;
    var flAuto = false;
    function doSearch(ev) {
        if (ev.keyCode == 13) {
            timeoutHnd = setTimeout(gridReload, 500);
            return;
        }
        if (!flAuto) return;
        // var elem = ev.target||ev.srcElement;
        if (timeoutHnd) {
            clearTimeout(timeoutHnd)
        }
        if ((ev.keyCode >= 65 && ev.keyCode <= 90) || ev.keyCode == 8 || ev.keyCode == 46 || (ev.keyCode >= 48 && ev.keyCode <= 57) || (ev.keyCode >= 96 && ev.keyCode <= 105)) {
            timeoutHnd = setTimeout(gridReload, 500)
        }
    }
    function gridReload() {
        var search = $('#SearchString').val();
        jQuery("#list").setGridParam({ url: "<%=Model.DataUrl %>?search=" + search, page: 1 }).trigger("reloadGrid");
    }
    function enableAutosubmit(state) {
        flAuto = state;
        jQuery("#submitButton").attr("disabled", state);
    } 
</script>
