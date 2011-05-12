using System;
using System.ComponentModel.DataAnnotations;
using Core.Web.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Web.Models
{
    public class WidgetLookAndFeelModel : IMappedModel<PageWidgetSettings, WidgetLookAndFeelModel>
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
        public long WidgetId { get; set; }

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
        /// Gets or sets the width value.
        /// </summary>
        /// <value>The width value.</value>
        [DataType("String")]
        public float? WidthValue { get; set; }

        /// <summary>
        /// Gets or sets the width unit.
        /// </summary>
        /// <value>The width unit.</value>
        [DataType("List")]
        public String WidthUnit { get; set; }

        /// <summary>
        /// Gets or sets the height value.
        /// </summary>
        /// <value>The height value.</value>
        [DataType("String")]
        public float? HeightValue { get; set; }

        /// <summary>
        /// Gets or sets the height unit.
        /// </summary>
        /// <value>The height unit.</value>
        [DataType("List")]
        public String HeightUnit { get; set; }

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

        public WidgetLookAndFeelModel MapFrom(PageWidgetSettings from)
        {
            SettingId = from.Id;
            WidgetId = from.Widget.Id;
            BackgroundColor = from.LookAndFeelSettings.BackgroundColor;
            FontFamily = from.LookAndFeelSettings.FontFamily;
            FontSizeValue = from.LookAndFeelSettings.FontSizeValue;
            FontSizeUnit = from.LookAndFeelSettings.FontSizeUnit;
            Color = from.LookAndFeelSettings.Color;
            WidthValue = from.LookAndFeelSettings.WidthValue;
            WidthUnit = from.LookAndFeelSettings.WidthUnit;
            HeightValue = from.LookAndFeelSettings.HeightValue;
            HeightUnit = from.LookAndFeelSettings.HeightUnit;
            OtherStyles = from.LookAndFeelSettings.OtherStyles;

            return this;
        }

        public PageWidgetSettings MapTo(PageWidgetSettings to)
        {
            to.LookAndFeelSettings.BackgroundColor = BackgroundColor;
            to.LookAndFeelSettings.FontFamily = FontFamily;
            to.LookAndFeelSettings.FontSizeValue = FontSizeValue;
            to.LookAndFeelSettings.FontSizeUnit = FontSizeUnit;
            to.LookAndFeelSettings.Color = Color;
            to.LookAndFeelSettings.WidthValue = WidthValue;
            to.LookAndFeelSettings.WidthUnit = WidthUnit;
            to.LookAndFeelSettings.HeightValue = HeightValue;
            to.LookAndFeelSettings.HeightUnit = HeightUnit;
            to.LookAndFeelSettings.OtherStyles = OtherStyles;
            return to;
        }

        #endregion
    }
}