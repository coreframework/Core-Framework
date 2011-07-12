using System;
using System.ComponentModel.DataAnnotations;
using Core.Forms.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.Forms.Models
{
    public class FormViewModel : IMappedModel<Form, FormViewModel>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [Required]
        public virtual String Title { get; set; }

        public virtual long Id { get; set; }

        public bool AllowManage { get; set; }

        public FormViewModel MapFrom(Form from)
        {
            Id = from.Id;
            Title = from.Title;
            return this;
        }

        public Form MapTo(Form to)
        {
            to.Id = Id;
            to.Title = Title;
            return to;
        }
    }
}