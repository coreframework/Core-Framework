// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Web;

namespace Framework.Mvc.Helpers
{
    /// <summary>
    /// Provides helper methods to save uploaded files.
    /// </summary>
    public static class UploadHelper
    {
        #region Fields

        /// <summary>
        /// Path to temporary file upload directorty.
        /// </summary>
        public static readonly String TempDirectoryPath = "~/temp/";

        /// <summary>
        /// Key for file uploader option.
        /// </summary>
        public static readonly String ResizeImageFlag = "resize_image";

        /// <summary>
        /// Key for file uploader option.
        /// </summary>
        public static readonly String ResizeImageWidth = "resize_image_width";

        /// <summary>
        /// Key for file uploader option.
        /// </summary>
        public static readonly String ResizeImageHeight = "resize_image_height";

        /// <summary>
        /// Key for file uploader option.
        /// </summary>
        public static readonly String GenerateThumbnailFlag = "generate_thumbnail";

        /// <summary>
        /// Key for file uploader option.
        /// </summary>
        public static readonly String ThumbnailWidth = "thumbnail_width";

        /// <summary>
        /// Key for file uploader option.
        /// </summary>
        public static readonly String ThumbnailHeight = "thumbnail_height";

        /// <summary>
        /// Key for upload options metadata.
        /// </summary>
        public static readonly String UploadOptionsKey = "upload_options";

        /// <summary>
        /// Key for file types description metadata.
        /// </summary>
        public static readonly String FileTypesDescriptionKey = "file_types_description";

        /// <summary>
        /// Key for file types metadata.
        /// </summary>
        public static readonly String FileTypesKey = "file_types";

        /// <summary>
        /// Key for default value metadata.
        /// </summary>
        public static readonly String DefaultValueKey = "default_value";

        /// <summary>
        /// Key for authentication cookie.
        /// </summary>
        public static readonly String AuthenticationCookieKey = "authentication_cookie";

        private const int BufferSize = 4096;

        private const String FileNameTemplate = "File{0}{1}";

        private const String ThumbnailFileNameTemplate = "Thumb{0}{1}";

        private const String TimeStampFormat = "yyyyMMddhhmmssfff";

        #endregion

        #region Helper members

        /// <summary>
        /// Saves posted file to temporary directory.
        /// </summary>
        /// <param name="file">The posted file.</param>
        /// <param name="fileName">The posted file name.</param>
        /// <param name="mapPath">The map path delegate. Should map virtual path to server path.</param>
        /// <returns>Virtual path of saved files.</returns>
        public static String SaveFile(HttpPostedFileBase file, String fileName, Func<String, String> mapPath)
        {
            var buffer = new byte[BufferSize];
            var targetFileName = GenerateFileName(Path.GetExtension(fileName));
            var targetFileVirtualPath = VirtualPathUtility.Combine(TempDirectoryPath, targetFileName);
            var targetFileServerPath = mapPath(targetFileVirtualPath);

            var targetDirectory = Path.GetDirectoryName(targetFileServerPath);
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            using (var output = new FileStream(targetFileServerPath, FileMode.Create))
            {
                int byteCount;
                do
                {
                    byteCount = file.InputStream.Read(buffer, 0, BufferSize);
                    output.Write(buffer, 0, byteCount);
                }
                while (byteCount != 0);
            }

            return targetFileVirtualPath;
        }

        /// <summary>
        /// Generates unique file name.
        /// </summary>
        /// <param name="extension">The target file extension.</param>
        /// <returns>Generated file name virtual path.</returns>
        public static String GenerateFileName(String extension)
        {
            return VirtualPathUtility.Combine(TempDirectoryPath, String.Format(FileNameTemplate, DateTime.UtcNow.ToString(TimeStampFormat), extension));
        }

        /// <summary>
        /// Generates unique thumbnail file name.
        /// </summary>
        /// <param name="extension">The target file extension.</param>
        /// <returns>Generated file name virtual path.</returns>
        public static String GenerateThumbnailFileName(String extension)
        {
            return VirtualPathUtility.Combine(TempDirectoryPath, String.Format(ThumbnailFileNameTemplate, DateTime.UtcNow.ToString(TimeStampFormat), extension));
        }

        #endregion
    }
}