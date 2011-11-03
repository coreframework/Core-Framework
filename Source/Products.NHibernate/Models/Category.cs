using System;
using Framework.Core.Localization;
using Framework.Facilities.NHibernate.Objects;

namespace Products.NHibernate.Models
{
    public class Category : LocalizableEntity<CategoryLocale>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public virtual String Title
        {
            get
            {
                return ((CategoryLocale)CurrentLocale).Title;
            }
            set { ((CategoryLocale)CurrentLocale).Title = value; }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual String Description
        {
            get
            {
                return ((CategoryLocale)CurrentLocale).Description;
            }
            set { ((CategoryLocale)CurrentLocale).Description = value; }
        }

        /// <summary>
        /// Gets or sets the Create Date
        /// </summary>
        public virtual DateTime? CreateDate { get; set; }


        #endregion

        public virtual Type LocaleType
        {
            get
            {
                return typeof(CategoryLocale);
            }
        }

        public override ILocale InitializeLocaleEntity()
        {
            return new CategoryLocale
            {
                Category = this,
                Culture = null
            };
        }
    }
}
