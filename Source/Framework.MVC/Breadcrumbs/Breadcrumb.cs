// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Breadcrumb.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Framework.MVC.Breadcrumbs
{
    /// <summary>
    /// Provides IBreadcrumb implementation.
    /// </summary>
    public class Breadcrumb : IBreadcrumb
    {
        /// <summary>
        /// Gets or sets the breadcrumb URL.
        /// </summary>
        /// <value>The breadcrumb URL.</value>
        public string Url
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the breadcrumb title.
        /// </summary>
        /// <value>The breadcrumb title.</value>
        public string Text
        {
            get; set;
        }
    }
}
