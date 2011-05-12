namespace Core.Framework.MEF.Contracts.Web
{
    /// <summary>
    /// Defines the contract for providing metadata for a route registrar.
    /// </summary>
    public interface IRouteRegistrarMetadata
    {
        #region Properties
        /// <summary>
        /// Gets the order in which the registrar must be processed.
        /// </summary>
        int Order { get; }
        #endregion
    }
}

