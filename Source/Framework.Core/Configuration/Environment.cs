// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Environment.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Core.Configuration
{
    /// <summary>
    /// Specifies application configuration.
    /// </summary>
    public enum Environment
    {
        /// <summary>
        /// The development environment is used on your development computer as you interact manually with the application.
        /// </summary>
        Development,

        /// <summary>
        /// The test environment is used to run automated tests.
        /// </summary>
        Test,

        /// <summary>
        /// The production environment is used when you deploy your application for the world to use.
        /// </summary>
        Production
    }
}