using System;
using System.IO;
using System.Text;
using System.Web;
using Core.Forms.NHibernate.Models;
using Framework.Core.Utilities.Email;

namespace Core.Forms.Helpers
{
    public class FormsMailer
    {
        #region Fields

        private const String FormsAnswerTemplate = "FormsAnswerTemplate.txt";

        private const String FormsAnswerValueTemplate = "FormsAnswerValueTemplate.txt";

        private const String FormsTemplatesDirectory = "Content\\Templates\\";

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

        #region Helper Methods

        /// <summary>
        /// Sends the form answer email.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="formAnswer">The form answer.</param>
        /// <returns></returns>
        public static bool SendFormAnswerEmail(FormBuilderWidget model, FormWidgetAnswer formAnswer)
        {
            var templatePath = Path.Combine(FormsPlugin.Instance.PluginDirectory + FormsTemplatesDirectory, FormsAnswerTemplate);
            var email = new MailTemplate(templatePath)
                                    {
                                        FromEmail = EmailSettings.FromEmail,
                                        Subject = HttpUtility.HtmlEncode(model.Title),
                                        ToEmail = model.SenderEmail
                                    };

            FillFormAnswers(email, model, formAnswer);
            return email.Send();
        }

        /// <summary>
        /// Fills the form answers.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="model">The model.</param>
        /// <param name="formAnswer">The form answer.</param>
        private static void FillFormAnswers(MailTemplate email, FormBuilderWidget model, FormWidgetAnswer formAnswer)
        {
            email.AppendParam("Title", model.Title);
            email.AppendParam("Date", formAnswer.CreateDate.ToString());

            if (formAnswer.AnswerValues == null) return;

            var path = Path.Combine(FormsPlugin.Instance.PluginDirectory + FormsTemplatesDirectory, FormsAnswerValueTemplate);

            if (File.Exists(path))
            {
                var streamTemplate = new StreamReader(File.OpenRead(path), Encoding.UTF8);
                var template = streamTemplate.ReadToEnd();
                streamTemplate.Close();

                //render form answer
                var answer = new StringBuilder();
                foreach (var item in formAnswer.AnswerValues)
                {
                    answer.Append(template.Replace("<#AnswerValue#>", String.Format("{0}: {1}", item.Field, item.Value)));
                }

                email.AppendParam("Content", answer.ToString(), false);
            }
        }

        #endregion
    }
}