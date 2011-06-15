using Core.Forms.NHibernate.Helpers;

namespace Core.Forms.NHibernate.Models
{
    public enum FormElementType
    {
        [ElementTypeDescription(false, true, true)]
        TextBox = 1,

        [ElementTypeDescription(false, true, true)]
        TextArea = 2,

        [ElementTypeDescription(true, true, false)]
        DropDownList = 3,

        [ElementTypeDescription(true, false, false)]
        RadioButtons = 4,

        [ElementTypeDescription(false, false, false)]
        CheckBox = 5,

        [ElementTypeDescription(false, false, false)]
        TextField = 6,

        [ElementTypeDescription(false, false, false)]
        Captcha = 7
    }
}
