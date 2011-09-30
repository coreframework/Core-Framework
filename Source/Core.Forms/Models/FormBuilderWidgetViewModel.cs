using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Core.Forms.NHibernate.Permissions.Operations;
using Core.Framework.Permissions.Extensions;
using Core.Framework.Permissions.Models;
using Framework.Core.DomainModel;
using Framework.Mvc.Metadata.Attributes;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Models
{
    public class FormBuilderWidgetViewModel : IMappedModel<FormBuilderWidget, FormBuilderWidgetViewModel>
    {
        #region Fields

        private List<Form> forms;
        private ICorePrincipal user;

        #endregion

        #region Properties

        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [StringLength(255), Required]
        public String Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [save data].
        /// </summary>
        /// <value><c>true</c> if [save data]; otherwise, <c>false</c>.</value>
        public bool SaveData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [send email].
        /// </summary>
        /// <value><c>true</c> if [send email]; otherwise, <c>false</c>.</value>
        public bool SendEmail { get; set; }

        /// <summary>
        /// Gets or sets the recipient email.
        /// </summary>
        /// <value>The recipient email.</value>
        [StringLength(255), Email]
        public String RecipientEmail { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>The user.</value>
        public ICorePrincipal User
        {
            get { return user ?? (user = HttpContext.Current.CorePrincipal()); }
        }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>The forms.</value>
        public List<Form> Forms
        {
            get
            {
                if (forms == null)
                {
                    var formService = ServiceLocator.Current.GetInstance<IFormService>();
                    forms = (List<Form>) formService.GetAllowedFormsByOperation(User, (int) FormOperations.AddToWidget);
                }
                return forms;
            }
        }

        /// <summary>
        /// Gets or sets the form id.
        /// </summary>
        /// <value>The form id.</value>
        [Required]
        public long FormId { get; set; }

        #endregion

        public FormBuilderWidgetViewModel MapFrom(FormBuilderWidget from)
        {
            Id = from.Id;
            Title = from.Title;
            SaveData = from.SaveData;
            SendEmail = from.SendEmail;
            RecipientEmail = from.RecipientEmail;

            FormId = from.Form != null ? from.Form.Id : 0;
            return this;
        }

        public FormBuilderWidget MapTo(FormBuilderWidget to)
        {
            to.Id = Id;
            to.Title = Title;
            to.SaveData = SaveData;
            to.SendEmail = SendEmail;
            to.RecipientEmail = RecipientEmail;
            to.Form = new Form
                          {
                Id = FormId
            };
            return to;
        }
    }
}