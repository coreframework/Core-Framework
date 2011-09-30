// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadController.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Framework.Core.Helpers.Images;
using Framework.Mvc.Controllers;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Microsoft.Practices.ServiceLocation;

namespace Core.Web.Controllers
{
    /// <summary>
    /// Handles file upload.
    /// </summary>
    public partial class UploadController : FrameworkController
    {
        #region Fields

        private const String FileKey = "Filedata";

        private const String FileNameKey = "Filename";

        private readonly IImageUtility imageUtility;

        #endregion

        #region Constructors
        public UploadController()
        {
            this.imageUtility = ServiceLocator.Current.GetInstance<IImageUtility>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="UploadController"/> class.
        /// </summary>
        /// <param name="imageUtility">The image utility.</param>
        public UploadController(IImageUtility imageUtility)
        {
            this.imageUtility = imageUtility;
        }

        #endregion

        #region Actions

        /// <summary>
        /// Save uploaded file to temporary directory.
        /// </summary>
        /// <param name="form">Posted http-form.</param>
        /// <returns>Message and generated thumbnail.</returns>
        [HttpPost]
        public virtual ActionResult File(FormCollection form)
        {
            var file = HttpContext.Request.Files[FileKey];
            var fileName = HttpContext.Request.Form[FileNameKey];

            try
            {
                var targetFileVirtualPath = VirtualPathUtility.ToAbsolute(UploadHelper.SaveFile(file, fileName, path => Server.MapPath(path)));

                return new JsonResult
                {
                    Data = new
                    {
                        FileTitle = fileName,
                        FileName = targetFileVirtualPath,
                        Thumbnail = String.Empty,
                    }
                };
            }
            catch (Exception e)
            {
                Logger.Error("Some error was occured while upload file.", e);
                throw new HttpException((int)HttpStatusCode.InternalServerError, Translate("Messages.UploadError"));
            }
        }
        
        /// <summary>
        /// Save uploaded image file to temporary directory.
        /// </summary>
        /// <param name="form">Posted http-form.</param>
        /// <returns>Message and path to uploaded image and generated thumbnail.</returns>
        [HttpPost]
        public virtual ActionResult Image(FormCollection form)
        {
            var file = HttpContext.Request.Files[FileKey];
            var fileName = form[FileNameKey];
            
            try
            {
                var targetFileVirtualPath = VirtualPathUtility.ToAbsolute(UploadHelper.SaveFile(file, fileName, path => Server.MapPath(path)));
                var targetFileServerPath = Server.MapPath(targetFileVirtualPath);

                ResizeImage(form, targetFileServerPath);
                var thumbnailFileVirtualPath = GenerateThumbnail(form, targetFileServerPath);

                return new JsonResult
                {
                    Data = new
                    {
                        FileTitle = fileName,
                        FileName = targetFileVirtualPath,
                        Thumbnail = thumbnailFileVirtualPath,
                    }
                };
            }
            catch (Exception e)
            {
                Logger.Error("Some error was occured while upload file.", e);
                throw new HttpException((int)HttpStatusCode.InternalServerError, Translate("Messages.UploadError"));
            }
        }

        #endregion

        #region Helper members

        private void ResizeImage(FormCollection form, String targetFileServerPath)
        {
            if (form.BooleanValue(UploadHelper.ResizeImageFlag))
            {
                int resizeWidth = form.IntValue(UploadHelper.ResizeImageWidth);
                int resizeHeight = form.IntValue(UploadHelper.ResizeImageHeight);
                if (resizeWidth > 0 || resizeHeight > 0)
                {
                    try
                    {
                        imageUtility.ResizeImage(targetFileServerPath, targetFileServerPath, resizeWidth, resizeHeight);
                    }
                    catch(Exception ex)
                    {
                        Logger.Error("Some error was occured while ResizeImage file.", ex);
                    }
                }
            }
        }

        private String GenerateThumbnail(FormCollection form, String imageFileServerPath)
        {
            var thumbnailFileVirtualPath = String.Empty;
            if (form.BooleanValue(UploadHelper.GenerateThumbnailFlag))
            {
                thumbnailFileVirtualPath = VirtualPathUtility.ToAbsolute(UploadHelper.GenerateThumbnailFileName(Path.GetExtension(imageFileServerPath)));
                var thumbnailFileServerPath = Server.MapPath(thumbnailFileVirtualPath);
                int resizeWidth = form.IntValue(UploadHelper.ThumbnailWidth);
                int resizeHeight = form.IntValue(UploadHelper.ThumbnailHeight);
                if (resizeWidth > 0 || resizeHeight > 0)
                {
                    imageUtility.ResizeImage(imageFileServerPath, thumbnailFileServerPath, resizeWidth, resizeHeight);
                }
            }
            return thumbnailFileVirtualPath;
        }

        #endregion
    }
}