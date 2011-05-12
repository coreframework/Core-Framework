using System;

namespace Core.Framework.MEF.Composition
{
    /// <summary>
    /// Provides dynamic creation of parts.
    /// </summary>
    /// <typeparam name="TPart">The type of part to create.</typeparam>
    public class PartFactory<TPart>
    {
        #region Fields

        private readonly Func<PartLifetimeContext<TPart>> @delegate;

        #endregion

        #region Constructor
        /// <summary>
        /// Initialises a new instance of <see cref="PartFactory{T}" />.
        /// </summary>
        /// <param name="delegate">The delegate used to create the part.</param>
        public PartFactory(Func<PartLifetimeContext<TPart>> @delegate)
        {
            Throw.Throw.IfArgumentNull(@delegate, "delegate");

            this.@delegate = @delegate;
            PartType = typeof(TPart);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the <see cref="Type" /> if the part.
        /// </summary>
        public Type PartType { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an instance of the <see cref="PartLifetimeContext{T}" /> used for managing the part.
        /// </summary>
        /// <returns></returns>
        public PartLifetimeContext<TPart> CreatePartLifetimeContext()
        {
            return @delegate();
        }

        /// <summary>
        /// Creates an instance of the part.
        /// </summary>
        /// <returns></returns>
        public TPart CreatePart()
        {
            return CreatePartLifetimeContext().ExportedValue;
        }
        #endregion
    }
}