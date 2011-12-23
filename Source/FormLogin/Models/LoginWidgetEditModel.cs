using System;
using Core.FormLogin.NHibernate.Models;
using Framework.Core.DomainModel;

namespace Core.FormLogin.Models
{
    public class LoginWidgetEditModel : IMappedModel<FormLoginWidget, LoginWidgetEditModel>
    {
        #region Properties

        public long Id { get; set; }

        public bool ShowTitle { get; set; }

        public String ShowTitleFieldName { get; set; }

        public bool ChildView { get; set; }

        #endregion

        #region Constructors

        public LoginWidgetEditModel()
        {
            ShowTitle = true;
        }

        #endregion

        #region Methods

        public LoginWidgetEditModel MapFrom(FormLoginWidget from)
        {
            Id = from.Id;
            ShowTitle = from.ShowTitle;

            return this;
        }

        public FormLoginWidget MapTo(FormLoginWidget to)
        {
            to.Id = Id;
            to.ShowTitle = ShowTitle;

            return to;
        }

        #endregion
    }
}