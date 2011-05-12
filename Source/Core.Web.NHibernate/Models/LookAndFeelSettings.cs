using System;
using FluentNHibernate.Data;

namespace Core.Web.NHibernate.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class LookAndFeelSettings : Entity
    {
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public virtual String BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>The font family.</value>
        public virtual String FontFamily { get; set; }

        /// <summary>
        /// Gets or sets the font size value.
        /// </summary>
        /// <value>The font size value.</value>
        public virtual float? FontSizeValue { get; set; }

        /// <summary>
        /// Gets or sets the font size unit.
        /// </summary>
        /// <value>The font size unit.</value>
        public virtual String FontSizeUnit { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public virtual String Color { get; set; }

        /// <summary>
        /// Gets or sets the width value.
        /// </summary>
        /// <value>The width value.</value>
        public virtual float? WidthValue { get; set; }

        /// <summary>
        /// Gets or sets the width unit.
        /// </summary>
        /// <value>The width unit.</value>
        public virtual String WidthUnit { get; set; }

        /// <summary>
        /// Gets or sets the height value.
        /// </summary>
        /// <value>The height value.</value>
        public virtual float? HeightValue { get; set; }

        /// <summary>
        /// Gets or sets the height unit.
        /// </summary>
        /// <value>The height unit.</value>
        public virtual String HeightUnit { get; set; }

        /// <summary>
        /// Gets or sets the other styles.
        /// </summary>
        /// <value>The other styles.</value>
        public virtual String OtherStyles { get; set; }
    }
}
