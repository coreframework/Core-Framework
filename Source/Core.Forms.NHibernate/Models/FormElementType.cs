using System.ComponentModel;
using Core.Forms.NHibernate.Helpers;

namespace Core.Forms.NHibernate.Models
{
    public enum FormElementType
    {
        [ElementTypeDescription(IsRequiredEnabled = true, IsValidationEnabled = true, IsMaxLengthEnabled = true)]
        TextBox = 1,

        [ElementTypeDescription(IsRequiredEnabled = true, IsValidationEnabled = true, IsMaxLengthEnabled = true)]
        TextArea = 2,

        [ElementTypeDescription(IsValuesEnabled = true, IsRequiredEnabled = true)]
        DropDownList = 3,

        [ElementTypeDescription(IsValuesEnabled = true)]
        RadioButtons = 4,

        CheckBox = 5,

        TextField = 6,

        Captcha = 7
    }
}
