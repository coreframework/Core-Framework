using System;

namespace Framework.Mvc.CustomElements
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomElement
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>The title.</value>
        String Title { get;}

        /// <summary>
        /// Gets a value indicating whether this instance is values enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is values enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsValuesEnabled { get;}

        /// <summary>
        /// Gets a value indicating whether this instance is required enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is required enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsRequiredEnabled { get;}

        /// <summary>
        /// Gets a value indicating whether this instance is max length enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is max length enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsMaxLengthEnabled { get;}
    }
}
