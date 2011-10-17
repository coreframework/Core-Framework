using Core.Forms.NHibernate.Helpers;

namespace Core.Forms.NHibernate.Models
{
    public enum FormElementType
    {
        [ElementTypeDescription(IsRequiredEnabled = true, IsValidationEnabled = true, IsMaxLengthEnabled = true)]
        TextBox,

        [ElementTypeDescription(IsRequiredEnabled = true, IsValidationEnabled = true, IsMaxLengthEnabled = true)]
        TextArea,

        [ElementTypeDescription(IsValuesEnabled = true, IsRequiredEnabled = true)]
        DropDownList,

        [ElementTypeDescription(IsValuesEnabled = true)]
        RadioButtons,

        CheckBox,

        TextField,

        Captcha
    }
}
