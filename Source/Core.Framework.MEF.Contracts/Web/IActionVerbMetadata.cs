using System;

namespace Core.Framework.MEF.Contracts.Web
{
    /// <summary>
    /// Defines metadata associated with an action verb.
    /// </summary>
    public interface IActionVerbMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the category for the verb.
        /// </summary>
        String Category { get; }
        #endregion
    }
}