// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileTypeAttribute.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Resources;

namespace Framework.Mvc.Metadata.Attributes
{
    /// <summary>
    /// Specify file types available for upload.
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class FileTypeAttribute : Attribute
    {
        #region Fields

        private static readonly Dictionary<FileTypesPreset, String> presetsFormats = new Dictionary<FileTypesPreset, String>
        {
            { FileTypesPreset.Images, "*.jpg;*.png;*.gif;*.bmp;" },
            { FileTypesPreset.Docs, "*.doc;*.docx;*.rtf;" },
            { FileTypesPreset.Excel, "*.xls;*.xlsx;*.xml;" },
        };

        private static readonly Dictionary<FileTypesPreset, String> presetsDescriptions = new Dictionary<FileTypesPreset, String>
        {
            { FileTypesPreset.Images, "Images" },
            { FileTypesPreset.Docs, "Documents" },
            { FileTypesPreset.Excel, "Excel spread sheets" },
        };

        private String description = String.Empty;

        private String formats = String.Empty;

        private FileTypesPreset preset;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the preset.
        /// </summary>
        /// <value>The preset.</value>
        public FileTypesPreset Preset
        {
            get
            {
                return preset;
            }
            set
            {
                preset = value;
            }
        }

        /// <summary>
        /// Gets or sets the formats.
        /// </summary>
        /// <value>The formats.</value>
        public String Formats
        {
            get
            {
                var result = String.Empty;

                if (Preset != FileTypesPreset.None && presetsFormats.ContainsKey(Preset))
                {
                    result = presetsFormats[Preset];
                }

                if (!String.IsNullOrEmpty(formats))
                {
                    result = formats;
                }

                return result;
            }
            set
            {
                formats = value;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public String Description
        {
            get
            {
                var result = String.Empty;

                if (Preset != FileTypesPreset.None && presetsDescriptions.ContainsKey(Preset))
                {
                    result = presetsDescriptions[Preset];
                }

                if (ResourceType != null && !String.IsNullOrEmpty(ResourceKey))
                {
                    var resourceManager = new ResourceManager(ResourceType);
                    result = resourceManager.GetString(ResourceKey);
                }

                if (!String.IsNullOrEmpty(description))
                {
                    result = description;
                }

                return result;
            }
            set
            {
                description = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the resources class for <see cref="Description"/>.
        /// </summary>
        /// <value>The type of the resource.</value>
        public Type ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the resource key for <see cref="Description"/>.
        /// </summary>
        /// <value>The resource key.</value>
        public String ResourceKey { get; set; }

        #endregion
    }
}