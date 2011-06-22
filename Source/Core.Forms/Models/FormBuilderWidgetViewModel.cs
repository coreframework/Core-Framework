using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core.Forms.NHibernate.Contracts;
using Core.Forms.NHibernate.Models;
using Framework.Core.DomainModel;
using Microsoft.Practices.ServiceLocation;

namespace Core.Forms.Models
{
    public class FormBuilderWidgetViewModel : IMappedModel<FormBuilderWidget, FormBuilderWidgetViewModel>
    {
        #region Fields

        private List<Form> _forms;

        #endregion

        #region Properties

        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public string Title { get; set; }

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
        /// Gets or sets the sender email.
        /// </summary>
        /// <value>The sender email.</value>
        public String SenderEmail { get; set; }

        /// <summary>
        /// Gets the forms.
        /// </summary>
        /// <value>The forms.</value>
        public List<Form> Forms
        {
            get
            {
                if (_forms == null)
                {
                    var formService = ServiceLocator.Current.GetInstance<IFormService>();
                    _forms = (List<Form>)formService.GetAll();
                }
                return _forms;
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
            SenderEmail = from.SenderEmail;

            FormId = from.Form != null ? from.Form.Id : 0;
            return this;
        }

        public FormBuilderWidget MapTo(FormBuilderWidget to)
        {
            to.Id = Id;
            to.Title = Title;
            to.SaveData = SaveData;
            to.SendEmail = SendEmail;
            to.SenderEmail = SenderEmail;
            to.Form = new Form
                          {
                Id = FormId
            };
            return to;
        }
    }
}