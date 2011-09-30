// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MetadataProvider.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Framework.Mvc.Extensions;
using Framework.Mvc.Helpers;
using Framework.Mvc.Metadata.Attributes;

namespace Framework.Mvc.Metadata
{
    /// <summary>
    /// Customize entity metadata provider.
    /// </summary>
    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        #region Fields

        private readonly bool localizeDisplayNames = true;

        private readonly bool localizeValidators = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataProvider"/> class.
        /// </summary>
        /// <param name="localizeDisplayNames">if set to <c>true</c> model display names should be localized.</param>
        /// <param name="localizeValidators">if set to <c>true</c> validation attributes error messages should be localized.</param>
        public MetadataProvider(bool localizeDisplayNames, bool localizeValidators)
        {
            this.localizeDisplayNames = localizeDisplayNames;
            this.localizeValidators = localizeValidators;
        }

        #endregion

        #region DataAnnotationsModelMetadataProvider members

        /// <summary>
        /// Gets the metadata for the specified property.
        /// </summary>
        /// <param name="attributes">The attributes.</param>
        /// <param name="containerType">The type of the container.</param>
        /// <param name="modelAccessor">The model accessor.</param>
        /// <param name="modelType">The type of the model.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The metadata for the property.</returns>
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<Object> modelAccessor, Type modelType, String propertyName)
        {
            var metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            // fixes metadata for not-nullable value-type fields
            metadata.IsRequired = attributes.OfType<RequiredAttribute>().Any();

            if (containerType != null)
            {
                if (localizeDisplayNames)
                {
                    LocalizeDisplayName(metadata, containerType, propertyName);
                }

                if (localizeValidators)
                {
                    LocalizeValidators(attributes, containerType, propertyName);
                }
            }

            ProcessFileUploadOptions(attributes, metadata);
            ProcessImageUploadOptions(attributes, metadata);

            return metadata;
        }

        private static void ProcessFileUploadOptions(IEnumerable<Attribute> attributes, ModelMetadata metadata)
        {
            var fileTypes = attributes.OfType<FileTypeAttribute>().FirstOrDefault();
            if (fileTypes != null)
            {
                if (!String.IsNullOrEmpty(fileTypes.Formats))
                {
                    metadata.AdditionalValues[UploadHelper.FileTypesKey] = fileTypes.Formats;
                }
                if (!String.IsNullOrEmpty(fileTypes.Description))
                {
                    metadata.AdditionalValues[UploadHelper.FileTypesDescriptionKey] = fileTypes.Description;
                }
            }
        }

        private static void ProcessImageUploadOptions(IEnumerable<Attribute> attributes, ModelMetadata metadata)
        {
            var imageUpload = attributes.OfType<ImageUploadAttribute>().FirstOrDefault();
            if (imageUpload != null)
            {
                var uploadOptions = new Dictionary<String, Object>();
                uploadOptions[UploadHelper.ResizeImageFlag] = imageUpload.Resize;
                uploadOptions[UploadHelper.ResizeImageWidth] = imageUpload.ResizeWidth;
                uploadOptions[UploadHelper.ResizeImageHeight] = imageUpload.ResizeHeight;
                uploadOptions[UploadHelper.GenerateThumbnailFlag] = imageUpload.GenerateThumbnail;
                uploadOptions[UploadHelper.ThumbnailWidth] = imageUpload.ThumbnailWidth;
                uploadOptions[UploadHelper.ThumbnailHeight] = imageUpload.ThumbnailHeight;
                metadata.AdditionalValues[UploadHelper.UploadOptionsKey] = uploadOptions;
                if (!String.IsNullOrEmpty(imageUpload.DefaultImage))
                {
                    metadata.AdditionalValues[UploadHelper.DefaultValueKey] = imageUpload.DefaultImage;
                }
            }
        }

        #endregion

        #region Helper methods

        private static void LocalizeDisplayName(ModelMetadata metadata, Type modelType, String propertyName)
        {
            metadata.DisplayName = new HttpContextWrapper(HttpContext.Current).DisplayNameFor(modelType, propertyName);
        }

        private static void LocalizeValidators(IEnumerable<Attribute> attributes, Type modelType, String propertyName)
        {
            foreach (var attribute in attributes.OfType<ValidationAttribute>())
            {
                var validatorKey = attribute.GetType().Name.Replace("Attribute", String.Empty);
                var errorMessage = ResourceHelper.TranslateErrorMessage(new HttpContextWrapper(HttpContext.Current), modelType, propertyName, validatorKey);
                if (!String.IsNullOrEmpty(errorMessage))
                {
                    attribute.ErrorMessage = errorMessage;
                }
            }
        }

        #endregion
    }
}