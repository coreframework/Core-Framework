// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizableEntity.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Framework.Facilities.NHibernate.Objects
{
    /// <summary>
    /// Describes localizable entity.
    /// </summary>
    /// <typeparam name="T">ILocale type.</typeparam>
    public abstract class LocalizableEntity<T> : Entity, ILocalizable<T> where T : ILocale
    {
        #region Fields

        private ILocale currentLocale;
        private IList<T> currentLocales = new List<T>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current locales.
        /// </summary>
        /// <value>The current locales.</value>
        public virtual IList<T> CurrentLocales
        {
            get
            {
                return currentLocales;
            }
            set
            {
                currentLocales = value;
            }
        }

        /// <summary>
        /// Gets the current entity locale.
        /// </summary>
        /// <value>The current locale.</value>
        public virtual ILocale CurrentLocale
        {
            get 
            {
                return currentLocale ?? (currentLocale = CultureHelper.GetCurrentLocale(CurrentLocales.ToList().ConvertAll(item => (ILocale)item)) ?? InitializeLocaleEntity());
            }
        }

        /// <summary>
        /// Determines whether the specified locale contains locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        /// <returns>
        ///     <c>true</c> if the specified locale contains locale; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool ContainsLocale(ILocale locale)
        {
            return CurrentLocales.Contains((T)locale);
        }

        /// <summary>
        /// Adds the locale.
        /// </summary>
        /// <param name="locale">The locale.</param>
        public virtual void AddLocale(ILocale locale)
        {
            CurrentLocales.Add((T)locale);
        }

        /// <summary>
        /// Initializes the new locale entity instance.
        /// </summary>
        /// <returns>New instance of locale.</returns>
        public abstract ILocale InitializeLocaleEntity();

        #endregion
    }
}
