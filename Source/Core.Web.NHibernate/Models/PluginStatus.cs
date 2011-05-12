namespace Core.Web.NHibernate.Models
{
    /// <summary>
    /// Defined plugin statuses 
    /// </summary>
    public enum PluginStatus
    {
        /// <summary>
        /// Plugin is registered in the system but not unstalled.
        /// </summary>
        NotInstalled,

        /// <summary>
        /// Plugin installed in the systems.
        /// </summary>
        Installed
    }
}
