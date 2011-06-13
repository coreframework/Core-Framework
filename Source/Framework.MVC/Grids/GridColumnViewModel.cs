// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GridColumnViewModel.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.MVC.Grids
{
    /// <summary>
    /// Grid column view model.
    /// </summary>
    public class GridColumnViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GridColumnViewModel"/> class.
        /// </summary>
        public GridColumnViewModel()
        {
            Align = "left";
            Sortable = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The column name.</value>
        public String Name { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The column index.</value>
        public String Index { get; set; }

        /// <summary>
        /// Gets or sets the align.
        /// </summary>
        /// <value>The column align.</value>
        public String Align { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnViewModel"/> is resizable.
        /// </summary>
        /// <value><c>true</c> if resizable; otherwise, <c>false</c>.</value>
        public bool Resizable { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The column width.</value>
        public int? Width { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnViewModel"/> is sortable.
        /// </summary>
        /// <value><c>true</c> if sortable; otherwise, <c>false</c>.</value>
        public bool Sortable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GridColumnViewModel"/> is hidden.
        /// </summary>
        /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
        public bool Hidden { get; set; }

        #endregion
    }
}