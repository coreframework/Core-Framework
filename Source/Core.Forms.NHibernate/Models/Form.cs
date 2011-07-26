using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Framework.Permissions.Helpers;
using Core.Framework.Permissions.Models;
using FluentNHibernate.Data;
using Framework.Core.Localization;

namespace Core.Forms.NHibernate.Models
{
    /// <summary>
    /// Describes form entity.
    /// </summary>
    [Export(typeof(IPermissible))]
    public class Form : Entity, IPermissible, ILocalizable
    {
        #region Fields

        private readonly IList<FormElement> _formElements;

        private IList<FormLocale> _currentFormLocales = new List<FormLocale>();
        private IList<ILocale> _currentLocales = new List<ILocale>();
        private FormLocale _currentLocale;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        public Form()
        {
            PermissionTitle = "Forms";
            Operations = OperationsHelper.GetOperations<FormOperations>();

            _formElements = new List<FormElement>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public virtual long? UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show submit button].
        /// </summary>
        /// <value><c>true</c> if [show submit button]; otherwise, <c>false</c>.</value>
        public virtual bool ShowSubmitButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show reset button].
        /// </summary>
        /// <value><c>true</c> if [show reset button]; otherwise, <c>false</c>.</value>
        public virtual bool ShowResetButton { get; set; }

        /// <summary>
        /// Gets or sets the form elements.
        /// </summary>
        /// <value>The form elements.</value>
        public virtual IEnumerable<FormElement> FormElements
        {
            get { return _formElements; }
        }

        public virtual String Title
        {
            get
            {
                return ((FormLocale)CurrentLocale).Title;
            }
            set { ((FormLocale)CurrentLocale).Title = value; }
        }

        #endregion

        #region IPermissible Members

        /// <summary>
        /// Gets or sets the permission title.
        /// </summary>
        /// <value>The permission title.</value>
        public virtual string PermissionTitle { get; set; }

        /// <summary>
        /// Gets or sets the permission groups.
        /// </summary>
        /// <value>The permission groups.</value>
        public virtual IEnumerable<IPermissionOperation> Operations { get; set; }

        public virtual IList<ILocale> CurrentLocales
        {
            get
            {
                if (_currentLocales.Count == 0 && _currentFormLocales.Count > 0)
                {
                    _currentLocales = _currentFormLocales.ToList().ConvertAll(mc => (ILocale)mc);
                }
                return _currentLocales;
            }
            set
            {
                _currentLocales = value;
            }
        }

        public virtual IList<FormLocale> CurrentFormLocales
        {
            get
            {
                return CurrentLocales.ToList().ConvertAll(mc => (FormLocale)mc);
            }
            set
            {
                CurrentLocales = value.ToList().ConvertAll(mc => (ILocale)mc);
            }
        }

        public virtual Type LocaleType
        {
            get
            {
                return typeof(FormLocale);
            }
        }

        public virtual ILocale CurrentLocale
        {
            get
            {
                if (_currentLocale == null)
                {
                    //2 - max locales number: current locale and default locale
                    if (CurrentLocales != null && CurrentLocales.Count > 0 && CurrentLocales.Count <= 2)
                    {
                        if (CurrentLocales.Count == 1)
                        {
                            _currentLocale = (FormLocale)CurrentLocales[0];
                        }
                        else if (!CurrentLocales[0].Culture.Equals(CultureHelper.DefaultCultureName))
                        {
                            _currentLocale = (FormLocale)CurrentLocales[0];
                        }
                        else
                        {
                            _currentLocale = (FormLocale)CurrentLocales[1];
                        }
                    }
                    else
                    {
                        _currentLocale = new FormLocale
                        {
                            Form = this,
                            Culture = CultureHelper.DefaultCultureName
                        };
                    }
                }
                return _currentLocale;
            }
        }

        #endregion
    }
}
