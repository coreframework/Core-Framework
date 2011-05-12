using System;

namespace Core.Framework.Plugins.Web
{
    /// <summary>
    ///  Defines a widget verb.
    /// </summary>
    public interface IWidgetActionVerb
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        String Action { get; }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        String Controller { get; }

        /// <summary>
        /// Gets the area.
        /// </summary>
        /// <value>The area.</value>
        String Area { get; }
    }
}
