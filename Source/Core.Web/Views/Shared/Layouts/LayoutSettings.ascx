<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Core.Web.Models.ChangeLayoutModel>" %>
<%@ Import Namespace="Core.Web.Helpers.Layouts" %>
<%@ Import Namespace="Core.Web.NHibernate.Models" %>
<div class="form_area">
    <fieldset>
        <%
            int rowIndex = 1;
            foreach (PageLayoutRow row in Model.CurrentLayout.LayoutTemplate.Rows)
            {%>
        <div class="columnWidthSliderHolder">
            <p>
                <label style="display: inline">
                    Row
                    <%=rowIndex++ %>:</label>
                <input type="text" class="columnWidthSliderAmount"/>
            </p>
            <div id="columnWidthSlider_<%=row.Id %>" class="columnWidthSlider" rowid="<%=row.Id %>">
            </div>
        </div>
        <script type="text/javascript">
    $(function () { $("#columnWidthSlider_<%=row.Id %>").slider({ disabled:
    <%= row.Columns.Count() == 1 ? "true" : "false" %>, range:
    <%= row.Columns.Count() > 2 ? "true" : "false"%>, 
    <%if(row.Columns.Count() > 2)
{%>
    values: [<%
    int col1Width = LayoutHelper.GetColumnWidth(Model.CurrentLayout, row.Columns.ElementAt(0));
    int col2Width = LayoutHelper.GetColumnWidth(Model.CurrentLayout, row.Columns.ElementAt(1)); %><%=col1Width %>, <%=col1Width + col2Width %>]
                <%
}else
{%>
    value: <%=LayoutHelper.GetColumnWidth(Model.CurrentLayout, row.Columns.First()) %>
<%
}%>,
slide: function( event, ui ) {
                $sliderHolder = $(ui.handle).parents('.columnWidthSliderHolder');
                $amount = $('.columnWidthSliderAmount', $sliderHolder); 
                if(ui.values && ui.values.length == 2) {
                    $amount.val( ui.values[ 0 ] + "% - " + (ui.values[ 1 ]-ui.values[ 0 ]) + "% - " + (100 - ui.values[ 1 ]) + "%");    
                }
                else {
                    $amount.val( ui.value + "% - " + (100 - ui.value) + "%");    
                }	
			},
            stop: function (event, ui) {
                $sliderDiv = $("#columnWidthSlider_<%=row.Id %>");
                var rowId = $sliderDiv.attr('rowId');
                var $row = $('tr[rowid=' + rowId + ']');
                var isRange = $sliderDiv.slider( "option", "range" );
                if(isRange) {
                    if($('td.column', $row).length == 3) {
                        var columnIndex = 0;
                        $('td.column', $row).each(function (){
                            if(columnIndex == 0) {
                                $(this).width($sliderDiv.slider("values", 0) + '%');
                            }
                            else if(columnIndex == 1){
                                $(this).width( ($sliderDiv.slider("values", 1) - $sliderDiv.slider("values", 0)) + '%');
                            }
                            else {
                                $(this).width( (100 - $sliderDiv.slider("values", 1)) + '%');
                            }
                            columnIndex++;
                        });
                    }                    
                }
                else{
                    if($('td.column', $row).length == 2) {
                        var columnIndex = 0;
                        $('td.column', $row).each(function (){
                            if(columnIndex == 0) {
                                $(this).width($sliderDiv.slider( "value" ) + '%');
                            }
                            else {
                                $(this).width((100 - $sliderDiv.slider( "value" )) + '%');
                            }
                            columnIndex++;
                        });
                    }                    
                }                
            }
    }); 
    $sliderDiv = $("#columnWidthSlider_<%=row.Id %>");
    $sliderHolder = $sliderDiv.parents('.columnWidthSliderHolder');
    $amount = $('.columnWidthSliderAmount', $sliderHolder);
    var isRange = $sliderDiv.slider( "option", "range" );
    if(isRange) {
        $amount.val( $sliderDiv.slider("values", 0) + "% - " + ($sliderDiv.slider("values", 1) - $sliderDiv.slider("values", 0)) + "% - " + (100 - $sliderDiv.slider("values", 1)) + "%");    
    }
    else if(!$sliderDiv.slider( "option", "disabled" )){
        $amount.val( $sliderDiv.slider( "value" ) + "% - " + (100 - $sliderDiv.slider( "value" )) + "%");    
    }
    else {
        $amount.val( "100%");
    }   
    });
        </script>
        <%
}%>
    </fieldset>
    <%:Html.AntiForgeryToken()%>
</div>
 <div class="p_footer clrfix">
	<div class="btn1"><em></em><%: Html.Submit("Save", new { Class = "button change-layout-btn" })%><strong></strong></div>
</div>
<script type="text/javascript">
    $(function () {
        $('.change-layout-btn', $('.form_area').parent()).click(function () {
            var postData = { 
                LayoutId: <%=Model.CurrentLayout.Id %>,
                RowsSetting: [] 
            };
            $(".columnWidthSlider").each(function () {
                var isRange = $(this).slider( "option", "range" );
                if(isRange) {
                    postData.RowsSetting.push({RowId: $(this).attr('rowId'), ColumnsWidth: [$(this).slider("values", 0), ($(this).slider("values", 1) - $(this).slider("values", 0)), (100 - $(this).slider("values", 1))]});
                }
                else if(!$(this).slider( "option", "disabled" )) {
                    postData.RowsSetting.push({RowId: $(this).attr('rowId'), ColumnsWidth: [$(this).slider( "value" ), (100 - $(this).slider( "value" ))]});
                }
                else
                {
                    postData.RowsSetting.push({RowId: $(this).attr('rowId'), ColumnsWidth: [100]});
                } 
            });
            $.ajax({
                type: "POST",
                contentType: 'application/json; charset=utf-8',
                url: "<%=Url.Action(MVC.Pages.UpdateLayoutSettingsForm()) %>",
                dataType: 'json',
                data: $.toJSON(postData),
                success: function (result) {
                    $('#successedResult').show();
                    if(result.IsSuccessed) {
                        $('#notSuccessedResult').hide();
                        $('#successedResult').show();
                    }
                    else {
                        $('#successedResult').hide();
                        $('#notSuccessedResult').show();
                    }
                }
            });
        }); 
    });
</script>
