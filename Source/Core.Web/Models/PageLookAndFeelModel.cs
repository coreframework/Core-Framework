using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Models
{
    public class PageLookAndFeelModel : IMappedModel<PageSettings, PageLookAndFeelModel>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the setting id.
        /// </summary>
        /// <value>The setting id.</value>
        public long SettingId { get; set; }

        /// <summary>
        /// Gets or sets the widget id.
        /// </summary>
        /// <value>The widget id.</value>
        public long PageId { get; set; }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        [DataType("ColorPicker")]
        public String BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>The font family.</value>
        [DataType("List")]
        public String FontFamily { get; set; }

        /// <summary>
        /// Gets or sets the font size value.
        /// </summary>
        /// <value>The font size value.</value>
        [DataType("String")]
        public float? FontSizeValue { get; set; }

        /// <summary>
        /// Gets or sets the font size unit.
        /// </summary>
        /// <value>The font size unit.</value>
        [DataType("List")]
        public String FontSizeUnit { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [DataType("ColorPicker")]
        public String Color { get; set; }

        /// <summary>
        /// Gets or sets the other styles.
        /// </summary>
        /// <value>The other styles.</value>
        [DataType("MultilineText")]
        public String OtherStyles { get; set; }

        #endregion

        #region Methods

        public PageLookAndFeelModel MapFrom(PageSettings from)
        {
            SettingId = from.Id;
            PageId = from.Page.Id;
            BackgroundColor = from.LookAndFeelSettings.BackgroundColor;
            FontFamily = from.LookAndFeelSettings.FontFamily;
            FontSizeValue = from.LookAndFeelSettings.FontSizeValue;
            FontSizeUnit = from.LookAndFeelSettings.FontSizeUnit;
            Color = from.LookAndFeelSettings.Color;
            OtherStyles = from.LookAndFeelSettings.OtherStyles;

            return this;
        }

        public PageSettings MapTo(PageSettings to)
        {
            to.LookAndFeelSettings.BackgroundColor = BackgroundColor;
            to.LookAndFeelSettings.FontFamily = FontFamily;
            to.LookAndFeelSettings.FontSizeValue = FontSizeValue;
            to.LookAndFeelSettings.FontSizeUnit = FontSizeUnit;
            to.LookAndFeelSettings.Color = Color;
            to.LookAndFeelSettings.OtherStyles = OtherStyles;
            return to;
        }

        #endregion
    }
}