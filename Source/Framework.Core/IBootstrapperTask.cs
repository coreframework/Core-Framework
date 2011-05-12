// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBootstrapperTask.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Castle.MicroKernel;

namespace Framework.Core
{
    /// <summary>
    /// Application start-up task.
    /// </summary>
    public interface IBootstrapperTask
    {
        /// <summary>
        /// Executes task.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="kernel">The kernel.</param>
        void Execute(IApplication application, IKernel kernel);
    }
}