using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Defines the event arguments used for <see cref="ExportProvider" /> events.
    /// </summary>
    internal class TaggedExportsChangeEventArgs : ExportsChangeEventArgs
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="TaggedExportsChangeEventArgs" />.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="added">The set of added exports.</param>
        /// <param name="removed">The set of removed exports.</param>
        /// <param name="atomicComposition">The atomic composition of the exports.</param>
        public TaggedExportsChangeEventArgs(object sender, IEnumerable<ExportDefinition> added, IEnumerable<ExportDefinition> removed, AtomicComposition atomicComposition)
            : base(added, removed, atomicComposition)
        {
            Sender = sender;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the sender of the event.
        /// </summary>
        public object Sender { get; private set; }
        #endregion
    }
}
