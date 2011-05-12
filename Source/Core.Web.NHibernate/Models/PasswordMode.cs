namespace Core.Web.NHibernate.Models
{
    /// <summary>
    /// Specifies password encryption mode and hash algorithm.
    /// </summary>
    public enum PasswordMode
    {
        /// <summary>
        /// Password is not encrypted.
        /// </summary>
        ClearText,

        /// <summary>
        /// Password is encrypted as MD5 hash.
        /// </summary>
        MD5,

        /// <summary>
        /// Password is encrypted as SHA1 hash.
        /// </summary>
        SHA1,

        /// <summary>
        /// Password is encrypted as SHA256 hash.
        /// </summary>
        SHA256,

        /// <summary>
        /// Password is encrypted as SHA384 hash.
        /// </summary>
        SHA384,

        /// <summary>
        /// Password is encrypted as SHA512 hash.
        /// </summary>
        SHA512
    }
}
