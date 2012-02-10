// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IModule.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Castle.MicroKernel.Registration;

namespace Framework.Core.Modules
{
    /// <summary>
    /// Specifies interface for module with web extensions.
    /// </summary>
    public interface IModule : IWindsorInstaller
    {
    }
}