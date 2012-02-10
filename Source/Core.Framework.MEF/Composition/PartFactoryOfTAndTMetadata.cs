using System;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Provides dynamic creation of parts.
    /// </summary>
    /// <typeparam name="TPart">The type of part to create.</typeparam>
    /// <typeparam name="TMetadata">The type of metadata for the part.</typeparam>
    public class PartFactory<TPart, TMetadata> : PartFactory<TPart>
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="PartFactory{TPart,TMetadata}" />.
        /// </summary>
        /// <param name="delegate">The delegate used to create the part.</param>
        /// <param name="metadata">The metadata for the part.</param>
        public PartFactory(Func<PartLifetimeContext<TPart>> @delegate, TMetadata metadata)
            : base(@delegate)
        {
            Metadata = metadata;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the metadata for the part.
        /// </summary>
        public TMetadata Metadata { get; private set; }

        #endregion
    }
}
