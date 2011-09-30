// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultImageUtility.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace Framework.Core.Helpers.Images
{
    /// <summary>
    /// Default <see cref="IImageUtility"/> implementation. Using GDI+ to process images.
    /// </summary>
    public class DefaultImageUtility : IImageUtility
    {
        #region Fields

        private const String DefaultMimeType = "image/jpeg";

        private static readonly Dictionary<String, String> mimeTypesMapping = new Dictionary<String, String>
                                                                                  {
                                                                                      { ".bmp", "image/bmp" },
                                                                                      { ".jpe", "image/jpeg" },
                                                                                      { ".jpeg", "image/jpeg" },
                                                                                      { ".jpg", "image/jpeg" },
                                                                                      { ".png", "image/png" },
                                                                                      { ".tiff", "image/tiff" },
                                                                                      { ".ico", "image/ico" },
                                                                                  };

        private static readonly float[][] watermarkColorMatrix =
            {
                new[] { 1.0f, 0.0f, 0.0f, 0.0f, 0.0f },
                new[] { 0.0f, 1.0f, 0.0f, 0.0f, 0.0f },
                new[] { 0.0f, 0.0f, 1.0f, 0.0f, 0.0f },
                new[] { 0.0f, 0.0f, 0.0f, 0.3f, 0.0f },
                new[] { 0.0f, 0.0f, 0.0f, 0.0f, 1.0f }
            };

        #endregion

        #region IImageUtility members

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="source">The input image.</param>
        /// <param name="resized">Target resized file path.</param>
        /// <param name="width">Width of the target.</param>
        /// <param name="height">Height of the target.</param>
        /// <remarks>
        /// If any of passed <paramref name="width"/> or <paramref name="height"/> is equal to 0 then parameter is excluded from calculations.
        /// If both parameters are equal to 0 then original image will serve as thumbnail.
        /// The result thumbnail will not be wider than <paramref name="width"/> and
        /// will not be higher than <paramref name="height"/>.
        /// </remarks>
        public void ResizeImage(Image source, String resized, int width, int height)
        {
            Check.IsNotNull(source, "source");
            Check.IsNotEmpty(resized, "resized");

            width = width < 0 ? 0 : width;
            height = height < 0 ? 0 : height;

            var sourceWidth = source.Width;
            var sourceHeight = source.Height;

            var sourceRatio = (double)sourceWidth / sourceHeight;
            var targetRatio = (double)width / height;

            int newWidth;
            int newHeight;

            if (targetRatio > sourceRatio)
            {
                newHeight = height;
                newWidth = (int)(height * sourceRatio);
            }
            else
            {
                newWidth = width;
                newHeight = (int)(width / sourceRatio);
            }

            using (var target = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                using (var canvas = Graphics.FromImage(target))
                {
                    int pasteX = (width - newWidth) / 2;
                    int pasteY = (height - newHeight) / 2;

                    canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    canvas.DrawImage(source, pasteX, pasteY, newWidth, newHeight);
                }

                source.Dispose();

                SaveImage(target, resized);
            }
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="source">The input stream.</param>
        /// <param name="resized">Target resized file path.</param>
        /// <param name="width">Width of the target.</param>
        /// <param name="height">Height of the target.</param>
        /// <remarks>
        /// If any of passed <paramref name="width"/> or <paramref name="height"/> is equal to 0 then parameter is excluded from calculations.
        /// If both parameters are equal to 0 then original image will serve as thumbnail.
        /// The result thumbnail will not be wider than <paramref name="width"/> and
        /// will not be higher than <paramref name="height"/>.
        /// </remarks>
        public void ResizeImage(Stream source, String resized, int width, int height)
        {
            Check.IsNotNull(source, "source");
            Check.IsNotEmpty(resized, "resized");

            using (var inputImage = Image.FromStream(source))
            {
                ResizeImage(inputImage, resized, width, height);
            }
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="source">Name of the source file.</param>
        /// <param name="resized">Name of the target file.</param>
        /// <param name="width">Width of the target. If it is equal to 0 then it will not be included into calculations.</param>
        /// <param name="height">Height of the target. If it is equal to 0 then it will not be included into calculations.</param>
        /// <remarks>
        /// If any of passed <paramref name="width"/> or <paramref name="height"/> is equal to 0 then parameter is excluded from calculations.
        /// If both parameters are equal to 0 then original image will serve as thumbnail.
        /// The result thumbnail will not be wider than <paramref name="width"/> and
        /// will not be higher than <paramref name="height"/>.
        /// </remarks>
        public void ResizeImage(String source, String resized, int width, int height)
        {
            Check.IsFileExists(source, "source");
            Check.IsNotEmpty(resized, "resized");

            using (var image = Image.FromFile(source))
            {
                ResizeImage(image, resized, width, height);
            }
        }

        /// <summary>
        /// Adds a watermark to <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source file.</param>
        /// <param name="watermark">The watermark file.</param>
        /// <param name="destination">The destination file.</param>
        public void AddWatermark(String source, String watermark, String destination)
        {
            Check.IsFileExists(source, "source");
            Check.IsFileExists(watermark, "watermark");
            Check.IsNotEmpty(destination, "destination");

            Bitmap result;

            // Creates an image object containing the source image.
            using (var sourceImage = Image.FromFile(source))
            {
                var width = sourceImage.Width;
                var height = sourceImage.Height;
                result = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                using (var canvas = Graphics.FromImage(result))
                {
                    canvas.DrawImage(sourceImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);

                    using (var watermarkImage = Image.FromFile(watermark))
                    {
                        // Define transparency settings over the watermark image
                        var imageAttributes = new ImageAttributes();

                        // Set an array with green color and zero color (alpha = 0)
                        // The watermark image will be drawn without pixels of the specified color (green).
                        var colorMap = new ColorMap
                                            {
                                                OldColor = Color.FromArgb(255, 0, 255, 0),
                                                NewColor = Color.FromArgb(0, 0, 0, 0)
                                            };

                        // Apply color changes. Create array to image Attributes.
                        imageAttributes.SetRemapTable(new[] { colorMap }, ColorAdjustType.Bitmap);
                        imageAttributes.SetColorMatrix(new ColorMatrix(watermarkColorMatrix),
                                                        ColorMatrixFlag.Default,
                                                        ColorAdjustType.Bitmap);

                        var x = width - watermarkImage.Width;
                        var y = height - watermarkImage.Height;

                        canvas.DrawImage(watermarkImage, new Rectangle(x, y, watermarkImage.Width, watermarkImage.Height), 0, 0, watermarkImage.Width, watermarkImage.Height, GraphicsUnit.Pixel, imageAttributes);
                    }
                }
            }

            SaveImage(result, destination);
        }

        #endregion

        #region Helper methods

        private void SaveImage(Bitmap image, String targetFile)
        {
            String targetDirectory = Path.GetDirectoryName(targetFile);
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var codecInfo = GetEncoderInfo(GetMimeType(targetFile));

            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);

            image.Save(targetFile, codecInfo, encoderParameters);
        }

        private static String GetMimeType(String fileName)
        {
            String mimeType;
            if (mimeTypesMapping.TryGetValue(Path.GetExtension(fileName).ToLowerInvariant(), out mimeType))
            {
                mimeType = DefaultMimeType;
            }
            return mimeType;
        }

        private ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().Where(codec => codec.MimeType == mimeType).FirstOrDefault();
        }

        #endregion
    }
}