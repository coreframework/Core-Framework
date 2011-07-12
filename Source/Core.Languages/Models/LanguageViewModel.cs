using System;
using System.ComponentModel.DataAnnotations;
using Core.Languages.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Languages.Models
{
    public class LanguageViewModel : IMappedModel<Language, LanguageViewModel>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [Required]
        public virtual String Code { get; set; }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>The culture.</value>
        [Required]
        public virtual String Culture { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsDefault { get; private set; }

        public LanguageViewModel MapFrom(Language from)
        {
            Title = from.Title;
            Code = from.Code;
            Culture = from.Culture;
            IsDefault = from.IsDefault;

            return this;

        }

        public Language MapTo(Language to)
        {
            to.Title = Title;
            to.Code = Code;
            to.Culture = Culture;

            return to;
        }

        #region Object members

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override String ToString()
        {
            if (!String.IsNullOrEmpty(Title))
            {
                return Title;
            }

            return base.ToString();
        }

        #endregion
    }
}