// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileTypesPreset.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Mvc.Metadata.Attributes
{
    /// <summary>
    /// Specifies predefined file types set.
    /// </summary>
    public enum FileTypesPreset
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None,

        /// <summary>
        /// Standard images formats (*.jpg, *.png, *.gif, *.bmp).
        /// </summary>
        Images,

        /// <summary>
        /// Document formats (*.doc, *.docx, *.rtf).
        /// </summary>
        Docs,

        /// <summary>
        /// Excel spread sheets (*.xls, *.xlsx, *.xml).
        /// </summary>
        Excel
    }
}