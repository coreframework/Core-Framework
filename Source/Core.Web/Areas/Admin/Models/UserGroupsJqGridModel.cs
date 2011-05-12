using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
/*using Trirand.Web.Mvc;*/

namespace Core.Web.Areas.Admin.Models
{
    public class UserGroupsJqGridModel
    {
        #region Properties

    /*    public JQGrid UserGroupsGrid { get; set; }*/

        #endregion

        #region Constructors

 /*       public UserGroupsJqGridModel()
        {
            UserGroupsGrid = new JQGrid
                                 {
                                     Columns = new List<JQGridColumn>()
                                                   {
                                                       new JQGridColumn
                                                           {
                                                               DataField = "Id",
                                                               // always set PrimaryKey for Add,Edit,Delete operations
                                                               // if not set, the first column will be assumed as primary key
                                                               PrimaryKey = true,
                                                               Editable = false,
                                                               Visible = false
                                                           },
                                                       new JQGridColumn
                                                           {
                                                               DataField = "Name",
                                                               Editable = false,
                                                               SearchType = SearchType.TextBox,
                                                               DataType = typeof (string),
                                                               SearchToolBarOperation = SearchOperation.Contains
                                                           },
                                                       new JQGridColumn
                                                           {
                                                               DataField = "Actions",
                                                               HtmlEncodeFormatString = true,
                                                               Sortable = false,
                                                               Width = 25,
                                                               Searchable = false
                                                           },
                                                       new JQGridColumn
                                                           {
                                                               DataField = "DetailsUrl",
                                                               Visible = false,
                                                       }
                                               },
                                 };
            UserGroupsGrid.AutoWidth = true;
            UserGroupsGrid.Height = Unit.Percentage(100);
            UserGroupsGrid.ToolBarSettings.ShowSearchToolBar = true;
            UserGroupsGrid.ToolBarSettings.ShowEditButton = false;
            UserGroupsGrid.ToolBarSettings.ShowAddButton = false;
            UserGroupsGrid.ToolBarSettings.ShowDeleteButton = false;
            UserGroupsGrid.ToolBarSettings.ShowRefreshButton = true;
            UserGroupsGrid.ClientSideEvents.RowSelect = "function() {location.href = $('tr[aria-selected=true] td').last().text();}";
        }*/

        #endregion
    }

    public class FormattedUserGroup
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Actions { get; set; }
        public string DetailsUrl { get; set; }
    }

}