// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementType.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using Framework.Mvc.ElementsTypes.Custom;
using Framework.Mvc.ElementsTypes.Generic;

namespace Framework.Mvc.ElementsTypes
{
    /// <summary>
    /// Describes element types.
    /// </summary>
    public enum ElementType
    {
        /// <summary>
        /// Text box element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        TextBox,

        /// <summary>
        /// Text area element type.
        /// </summary>
        [TextArea]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        TextArea,

        /// <summary>
        /// Select box element type.
        /// </summary>
        [SelectBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = true)]
        SelectBox,

        /// <summary>
        /// Radio buttons element type.
        /// </summary>
        [RadioButtons]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = true)]
        RadioButtons,

        /// <summary>
        /// Checkbox element type.
        /// </summary>
        [CheckBox]
        [ElementDescription(IsRequiredEnabled = false, IsValuesEnabled = false)]
        CheckBox,

        /// <summary>
        /// Date element type.
        /// </summary>
        [Date]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Date,

        /// <summary>
        /// Captcha element type.
        /// </summary>
        [ElementDescription(IsRequiredEnabled = false, IsValuesEnabled = true)]
        [Captcha]
        Captcha,

        /// <summary>
        /// First name element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        FirstName,

        /// <summary>
        /// Last name element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        LastName,

        /// <summary>
        /// Email element type.
        /// </summary>
        [Email]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Email,

        /// <summary>
        /// Gender element type.
        /// </summary>
        [Gender]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Gender,

        /// <summary>
        /// Birthdate element type.
        /// </summary>
        [Birthdate]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Birthdate,

        /// <summary>
        /// About me element type.
        /// </summary>
        [TextArea]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        AboutMe,

        /// <summary>
        /// Website element type.
        /// </summary>
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Website,

        /// <summary>
        /// Twitter element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Twitter,

        /// <summary>
        /// Facebook element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Facebook,

        /// <summary>
        /// City element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        City,

        /// <summary>
        /// Country element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Country,

        /// <summary>
        /// Zip code element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        ZipCode,

        /// <summary>
        /// Location element type.
        /// </summary>
        [TextBox]
        [ElementDescription(IsRequiredEnabled = true, IsValuesEnabled = false)]
        Location
    }
}
