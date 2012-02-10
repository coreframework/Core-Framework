using System;
using System.ComponentModel;

namespace Core.Framework.MEF.Contracts.Web
{
    /// <summary>
    /// Defines the contract for providing metadata for a controller.
    /// </summary>
    public interface IControllerMetadata
    {
        #region Properties

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <value>The area.</value>
        [DefaultValue("")]
        String Area { get; }

        #endregion
    }
}
