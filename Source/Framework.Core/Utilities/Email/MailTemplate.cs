﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Framework.Core.Utilities.Email
{
    /// <summary>
    /// Template email class.
    /// </summary>
    public class MailTemplate
    {
        #region Fields

        public const String ParamTemplate = "<#{0}#>";

        public const String SubjectParam = "Subject";

        private string bodyTemplate = String.Empty;

        private string from = String.Empty;

        private string fromEmail = String.Empty;

        private string to = String.Empty;

        private string toEmail = String.Empty;

        private string subject = String.Empty;

        private bool isHtmlFormat = true;

        private static MailConfiguration _emailSettings;

        private readonly MailMessage message = new MailMessage();

        private readonly Dictionary<String, String> parameters = new Dictionary<String, String>();

        private MailAddressCollection mailAddressCollection = new MailAddressCollection();

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

        /// <summary>
        /// From name.
        /// </summary>
        public string From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
            }
        }

        /// <summary>
        /// From email.
        /// </summary>
        public string FromEmail
        {
            get
            {
                return fromEmail;
            }
            set
            {
                fromEmail = value;
            }
        }

        /// <summary>
        /// To name.
        /// </summary>
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
            }
        }

        /// <summary>
        /// To email.
        /// </summary>
        public string ToEmail
        {
            get
            {
                return toEmail;
            }
            set
            {
                toEmail = value;
            }
        }

        /// <summary>
        /// Email subject.
        /// </summary>
        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                subject = value;
            }
        }

        /// <summary>
        /// Is HTML format enabled.
        /// </summary>
        public bool IsHtmlFormat
        {
            get
            {
                return isHtmlFormat;
            }
            set
            {
                isHtmlFormat = value;
            }
        }

        /// <summary>
        /// Path to attachment.
        /// </summary>
        public AttachmentCollection Attachments
        {
            get
            {
                return message.Attachments;
            }
        }

        /// <summary>
        /// Gets the SMTP host.
        /// </summary>
        /// <value>The SMTP host.</value>
        public virtual String SmtpHost
        {
            get
            {
                return EmailSettings.SmtpServer;
            }
        }

        /// <summary>
        /// Gets the SMTP user.
        /// </summary>
        /// <value>The SMTP user.</value>
        public virtual String SmtpUser
        {
            get
            {
                return EmailSettings.SmtpUser;
            }
        }

        /// <summary>
        /// Gets the SMTP password.
        /// </summary>
        /// <value>The SMTP password.</value>
        public virtual String SmtpPassword
        {
            get
            {
                return EmailSettings.SmtpPassword;
            }
        }

        /// <summary>
        /// Gets the body.
        /// </summary>
        /// <value>The body.</value>
        public String Body
        {
            get
            {
                foreach (KeyValuePair<string, string> pair in parameters)
                {
                    bodyTemplate = bodyTemplate.Replace(String.Format(ParamTemplate, pair.Key), pair.Value);
                }

                return bodyTemplate;
            }
        }

        /// <summary>
        /// Collection of target email addresses.
        /// </summary>
        public MailAddressCollection MailAddressCollection
        {
            get
            {
                return mailAddressCollection;
            }
            set
            {
                mailAddressCollection = value;
            }
        }

        /// <summary>
        /// Gets or sets the template directory.
        /// </summary>
        /// <value>The template directory.</value>
        public string TemplateDirectory { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MailTemplate"/> class.
        /// </summary>
        /// <param name="templateDirectory">The template directory.</param>
        /// <param name="bodyTemplate">The body template.</param>
        public MailTemplate(string templateDirectory, string bodyTemplate)
        {
            this.TemplateDirectory = templateDirectory;
            ReadTemplates(bodyTemplate);
        }

        /// <summary>
        /// Initialize new instance of MailTemplate.
        /// </summary>
        /// <param name="bodyTemplateFile">Name of the body template.</param>
        protected void ReadTemplates(String bodyTemplateFile)
        {
            if (!String.IsNullOrEmpty(bodyTemplateFile))
            {
                var streamBody = new StreamReader(File.OpenRead(GetFullTemplatePath(bodyTemplateFile)), Encoding.UTF8);
                bodyTemplate = streamBody.ReadToEnd();
                streamBody.Close();
            }
        }

        #endregion

        /// <summary>
        /// Add attachment.
        /// </summary>
        /// <param name="path">Path to the file.</param>
        public void AddAttachment(String path)
        {
            Attachments.Add(new Attachment(path));
        }

        /// <summary>
        /// Gets the full template path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private String GetFullTemplatePath(String path)
        {
            if (File.Exists(path))
            {
                return path;
            }

            if (HttpContext.Current != null)
            {
                return Path.Combine(HttpContext.Current.Server.MapPath("~/"), TemplateDirectory + path);
            }

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TemplateDirectory + path);
        }

        /// <summary>
        /// Appends param with its value to the email.
        /// </summary>
        /// <param name="key">Param Key</param>
        /// <param name="value">Param value.</param>
        public void AppendParam(String key, String value)
        {
            AppendParam(key, value, true);
        }

        /// <summary>
        /// Apends param to the email.
        /// </summary>
        /// <param name="key">Param key.</param>
        /// <param name="value">Param value.</param>
        /// <param name="encode">If true. the param value will be encided.</param>
        public void AppendParam(String key, String value, bool encode)
        {
            if (encode)
            {
                parameters[key] = HttpUtility.HtmlEncode(value);
            }
            else
            {
                parameters[key] = value;
            }
        }

        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="smtpHost">The SMTP host.</param>
        /// <param name="user">The SMTP user.</param>
        /// <param name="password">The SMTP password.</param>
        /// <returns></returns>
        public bool Send(String smtpHost, String user, String password)
        {
            var mailer = new EmailSender(smtpHost, user, password);

            if (mailer.SendEmail(from, fromEmail, to, toEmail, subject, Body, Attachments, IsHtmlFormat))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Send email.
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            return Send(SmtpHost, SmtpUser, SmtpPassword);
        }

        /// <summary>
        /// Send email with looging.
        /// </summary>
        /// <param name="enableLogging"></param>
        /// <returns></returns>
        public bool Send(bool enableLogging)
        {
            var mailer = new EmailSender(SmtpHost, SmtpUser, SmtpPassword);

            if (mailer.SendEmail(from, fromEmail, to, toEmail, subject, Body, Attachments, IsHtmlFormat))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        /// Sends mails to list of recipients specified in MailAddressCollection.
        /// </summary>
        /// <returns></returns>
        public bool SendMails()
        {
            var mailer = new EmailSender(SmtpHost, SmtpUser, SmtpPassword);

            if (mailer.SendEmail(from, fromEmail, mailAddressCollection, subject, Body, Attachments, IsHtmlFormat))
            {
                return true;
            }

            return false;
        }
    }
}
