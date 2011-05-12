using System;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Defines a context that manages the lifetime of a part.
    /// </summary>
    /// <typeparam name="T">The type of part.</typeparam>
    public class PartLifetimeContext<T> : IDisposable
    {
        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="PartLifetimeContext{T}" />.
        /// </summary>
        /// <param name="exportedValue">The instance of the exported value.</param>
        /// <param name="dispose">The delegate used for disposal.</param>
        public PartLifetimeContext(T exportedValue, Action dispose)
        {
            ExportedValue = exportedValue;
            DisposeAction = dispose;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the exported value.
        /// </summary>
        public T ExportedValue { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="Action" /> used for disposal.
        /// </summary>
        private Action DisposeAction { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Releases managed resources used by this instance.
        /// </summary>
        void IDisposable.Dispose()
        {
            if (DisposeAction != null)
                DisposeAction();
        }
        #endregion
    }
}