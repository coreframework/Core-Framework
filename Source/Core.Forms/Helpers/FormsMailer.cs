using System;
using System.Reflection;
using System.Text;
using System.Web;
using Core.Forms.NHibernate.Models;
using Framework.Core.Utilities.Email;

namespace Core.Forms.Helpers
{
    public class FormsMailer
    {
        #region Fields

        private const String FormsTemplate = "FormsTemplate.txt";

        private const String TemplateDirectory = "Content\\Templates\\";

        private const String FormsEmailSubjectTemplate = "{0}: {1}";

        private static MailConfiguration _emailSettings;

        #endregion

        #region Properties

        private static MailConfiguration EmailSettings
        {
           get
           {
               if (_emailSettings == null)
               {
                   _emailSettings = MailConfiguration.Current;
               }
               return _emailSettings;
           }
        }

        #endregion

        #region HelperMethods

        public static bool SendFormAnswerEmail(FormBuilderWidget model, FormWidgetAnswer formAnswer)
        {
            var email = new MailTemplate(TemplateDirectory, FormsTemplate)
                                    {
                                        FromEmail = EmailSettings.FromEmail,
                                        Subject = String.Format(FormsEmailSubjectTemplate, "Core-Framework", HttpUtility.HtmlEncode(model.Title)),
                                        ToEmail = model.SenderEmail
                                    };
            FillFormAnswers(email, model, formAnswer);
            return email.Send();
        }

        private static void FillFormAnswers(MailTemplate email, FormBuilderWidget model, FormWidgetAnswer formAnswer)
        {
            email.AppendParam("Title", model.Title);
            
            //render form answer
            var answer = new StringBuilder();
            foreach (var item in formAnswer.AnswerValues)
            {
                answer.Append(String.Format("{0}: {1}", item.Field, item.Value));
            }

            email.AppendParam("Title", answer.ToString());
        }

        #endregion
    }
}