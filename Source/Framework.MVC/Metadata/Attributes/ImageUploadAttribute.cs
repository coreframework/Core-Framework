// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageUploadAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Framework.MVC.Metadata.Attributes
{
    /// <summary>
    /// Specifies image upload options.
    /// </summary>
    public class ImageUploadAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the default image.
        /// </summary>
        /// <value>The default image.</value>
        public String DefaultImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ImageUploadAttribute"/> is resize.
        /// </summary>
        /// <value><c>true</c> if resize; otherwise, <c>false</c>.</value>
        public bool Resize { get; set; }

        /// <summary>
        /// Gets or sets the width of the resize.
        /// </summary>
        /// <value>The width of the resize.</value>
        public int ResizeWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the resize.
        /// </summary>
        /// <value>The height of the resize.</value>
        public int ResizeHeight { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [generate thumbnail].
        /// </summary>
        /// <value><c>true</c> if [generate thumbnail]; otherwise, <c>false</c>.</value>
        public bool GenerateThumbnail { get; set; }

        /// <summary>
        /// Gets or sets the width of the thumbnail.
        /// </summary>
        /// <value>The width of the thumbnail.</value>
        public int ThumbnailWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the thumbnail.
        /// </summary>
        /// <value>The height of the thumbnail.</value>
        public int ThumbnailHeight { get; set; }
    }
}