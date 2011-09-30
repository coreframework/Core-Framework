// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridViewModel.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Framework.Mvc.Grids
{
    /// <summary>
    /// Grid view model.
    /// </summary>
    public class GridViewModel
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewModel"/> class.
        /// </summary>
        public GridViewModel()
        {
            Columns = new List<GridColumnViewModel>();
            SelectedIds = new List<long>();
            IsAsc = true;
            SearchEnable = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the data URL.
        /// </summary>
        /// <value>The data URL.</value>
        public String DataUrl { get; set; }

        /// <summary>
        /// Gets or sets the details URL.
        /// </summary>
        /// <value>The details URL.</value>
        public String DetailsUrl { get; set; }

        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        /// <value>The search string.</value>
        public String SearchString { get; set; }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>The columns.</value>
        public IList<GridColumnViewModel> Columns { get; set; }

        /// <summary>
        /// Gets or sets the default order column.
        /// </summary>
        /// <value>The default order column.</value>
        public String DefaultOrderColumn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is asc.
        /// </summary>
        /// <value><c>true</c> if this instance is asc; otherwise, <c>false</c>.</value>
        public bool IsAsc { get; set; }

        /// <summary>
        /// Gets or sets the grid title.
        /// </summary>
        /// <value>The grid title.</value>
        public String GridTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [search enable].
        /// </summary>
        /// <value><c>true</c> if [search enable]; otherwise, <c>false</c>.</value>
        public bool SearchEnable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [multi select].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [multi select]; otherwise, <c>false</c>.
        /// </value>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [row clickable].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [row clickable]; otherwise, <c>false</c>.
        /// </value>
        public bool IsRowNotClickable { get; set; }

        /// <summary>
        /// Gets or sets the selected ids.
        /// </summary>
        /// <value>
        /// The selected ids.
        /// </value>
        public IEnumerable<long> SelectedIds { get; set; }

        /// <summary>
        /// Gets or sets the module title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public String Title { get; set; }

        #endregion
    }
}