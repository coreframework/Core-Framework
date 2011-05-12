// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMappedModel.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.DomainModel
{
    /// <summary>
    /// Specifies that object of <typeparamref name="TTargetModel"/> can be mapped from <typeparamref name="TSourceModel"/> and vise versa.
    /// </summary>
    /// <typeparam name="TSourceModel">The type of source model.</typeparam>
    /// <typeparam name="TTargetModel">The type of target model.</typeparam>
    public interface IMappedModel<TSourceModel, TTargetModel>
    {
        /// <summary>
        /// Maps current instance from source model.
        /// </summary>
        /// <param name="from">Source model.</param>
        /// <returns>Mapped target model.</returns>
        TTargetModel MapFrom(TSourceModel from);

        /// <summary>
        /// Maps current instance to source model.
        /// </summary>
        /// <param name="to">Source model.</param>
        /// <returns>Mapped source model.</returns>
        TSourceModel MapTo(TSourceModel to);
    }
}