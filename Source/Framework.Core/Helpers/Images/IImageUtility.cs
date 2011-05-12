// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImageUtility.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.IO;

namespace Framework.Core.Helpers.Images
{
    /// <summary>
    /// Specifies image processing interface.
    /// </summary>
    public interface IImageUtility
    {
        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="source">The input image.</param>
        /// <param name="resized">Target resized file path.</param>
        /// <param name="width">Width of the resized image. If it is equal to 0 then it will not be included into calculations.</param>
        /// <param name="height">Height of the resized image. If it is equal to 0 then it will not be included into calculations.</param>
        /// <remarks>
        /// If any of passed <paramref name="width"/> or <paramref name="height"/> is equal to 0 then parameter is excluded from calculations.
        /// If both parameters are equal to 0 then original image will serve as thumbnail.
        /// The result thumbnail will not be wider than <paramref name="width"/> and
        /// will not be higher than <paramref name="height"/>.
        /// </remarks>
        void ResizeImage(Image source, String resized, int width, int height);

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="source">The input stream.</param>
        /// <param name="resized">Target resized file path.</param>
        /// <param name="width">Width of the resized image. If it is equal to 0 then it will not be included into calculations.</param>
        /// <param name="height">Height of the resized image. If it is equal to 0 then it will not be included into calculations.</param>
        /// <remarks>
        /// If any of passed <paramref name="width"/> or <paramref name="height"/> is equal to 0 then parameter is excluded from calculations.
        /// If both parameters are equal to 0 then original image will serve as thumbnail.
        /// The result thumbnail will not be wider than <paramref name="width"/> and
        /// will not be higher than <paramref name="height"/>.
        /// </remarks>
        void ResizeImage(Stream source, String resized, int width, int height);

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="source">Name of the source file.</param>
        /// <param name="resized">Target resized file path.</param>
        /// <param name="width">Width of the resized image. If it is equal to 0 then it will not be included into calculations.</param>
        /// <param name="height">Height of the resized image. If it is equal to 0 then it will not be included into calculations.</param>
        /// <remarks>
        /// If any of passed <paramref name="width"/> or <paramref name="height"/> is equal to 0 then parameter is excluded from calculations.
        /// If both parameters are equal to 0 then original image will serve as thumbnail.
        /// The result thumbnail will not be wider than <paramref name="width"/> and
        /// will not be higher than <paramref name="height"/>.
        /// </remarks>
        void ResizeImage(String source, String resized, int width, int height);

        /// <summary>
        /// Adds a watermark to <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source file.</param>
        /// <param name="watermark">The watermark file.</param>
        /// <param name="destination">The destination file.</param>
        void AddWatermark(String source, String watermark, String destination);
    }
}