using System.ComponentModel;
using Framework.Core.DomainModel;

namespace Core.Framework.NHibernate.Models
{
    /// <summary>
    /// Represents users statuses.
    /// </summary>
    public enum UserStatus
    {
        /// <summary>
        /// User active.
        /// </summary>
        [Description("Admin.Models.UserStatus.Active")]
        Active,

        /// <summary>
        /// User locked.
        /// </summary>
        [Description("Admin.Models.UserStatus.Locked")]
        Locked,

        /// <summary>
        /// User deleted.
        /// </summary>
        [Description("Admin.Models.UserStatus.Deleted"), ExcludeItem]
        Deleted
    }
}
