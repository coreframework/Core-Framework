using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Framework.Core.Utilities.Email
{
    /// <summary>
    /// Summary description for OutsideMailer.
    /// </summary>
    public class EmailSender
    {
        private readonly String mailServer;

        private readonly String mailUser;

        private readonly String mailPassword;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailSender"/> class.
        /// </summary>
        /// <param name="server">The server.</param>
        /// <param name="login">The login.</param>
        /// <param name="psw">The PSW.</param>
        public EmailSender(String server, String login, String psw)
        {
            mailServer = server;
            mailUser = login;
            mailPassword = psw;
        }

        /// <summary>
        /// Sends emails to the specified address.
        /// </summary>
        /// <param name="from">From name.</param>
        /// <param name="fromEmail">From email.</param>
        /// <param name="to">To name.</param>
        /// <param name="toEmail">To email.</param>
        /// <param name="subject">Email subject</param>
        /// <param name="text">Email body.</param>
        /// <param name="attachPath">Path to the Attachments.</param>
        /// <param name="isHtmlFormat">Send in html format.</param>
        /// <returns>True if succedeed.</returns>
        public bool SendEmail(String from, String fromEmail, String to, String toEmail, String subject, String text,
                              AttachmentCollection attachPath, bool isHtmlFormat)
        {
            try
            {
                var recipients = new MailAddressCollection
                                     {
                                         new MailAddress(toEmail, to)
                                     };
                return SendEmail(from, fromEmail, recipients, subject, text, attachPath, isHtmlFormat);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Sends emails to the specified address.
        /// </summary>
        /// <param name="from">From name.</param>
        /// <param name="fromEmail">From email.</param>
        /// <param name="toEmails">Collection of To emails.</param>
        /// <param name="subject">Email subject</param>
        /// <param name="text">Email body.</param>
        /// <param name="attachPath">Path to the attachment.</param>
        /// <param name="isHtmlFormat">Send in html format.</param>
        /// <returns>True if succedeed.</returns>
        public bool SendEmail(String from, String fromEmail, IEnumerable<MailAddress> toEmails, String subject,
                              String text, AttachmentCollection attachPath, bool isHtmlFormat)
        {

            var message = new MailMessage();

            if (attachPath != null && attachPath.Count > 0)
            {
                foreach (Attachment att in attachPath)
                {
                    message.Attachments.Add(att);
                }
            }

            message.From = new MailAddress(fromEmail, from);
            foreach (MailAddress address in toEmails)
            {
                message.To.Add(address);
            }
            message.Subject = subject;
            message.IsBodyHtml = isHtmlFormat;
            message.Body = text;

            if (!isHtmlFormat)
            {
                message.BodyEncoding = Encoding.ASCII;
            }
            else
            {
                message.BodyEncoding = Encoding.UTF8;
            }

            return SendMail(message);
        }

        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="fromEmail">From email.</param>
        /// <param name="toEmails">To emails.</param>
        /// <param name="ccEmails">The cc emails.</param>
        /// <param name="bccEmails">The BCC emails.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="text">The text.</param>
        /// <param name="attach">The attach.</param>
        /// <param name="isHtmlFormat">if set to <c>true</c> [is HTML format].</param>
        /// <returns></returns>
        public bool SendEmail(String from, String fromEmail, IEnumerable<MailAddress> toEmails,
            IEnumerable<MailAddress> ccEmails, IEnumerable<MailAddress> bccEmails,
            String subject, String text, Attachment attach, bool isHtmlFormat)
        {

            var message = new MailMessage();

            if (attach != null)
            {
                message.Attachments.Add(attach);
            }

            message.From = new MailAddress(fromEmail, from);
            foreach (MailAddress address in toEmails)
            {
                message.To.Add(address);
            }

            foreach (MailAddress address in ccEmails)
            {
                message.CC.Add(address);
            }

            foreach (MailAddress address in bccEmails)
            {
                message.Bcc.Add(address);
            }

            message.Subject = subject;
            message.IsBodyHtml = isHtmlFormat;
            message.Body = text;

            if (!isHtmlFormat)
            {
                message.BodyEncoding = Encoding.ASCII;
            }
            else
            {
                message.BodyEncoding = Encoding.UTF8;
            }

            return SendMail(message);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private bool SendMail(MailMessage message)
        {
            var smtpClient = new SmtpClient
                                 {
                                     UseDefaultCredentials = false,
                                     Credentials = new NetworkCredential(mailUser, mailPassword),
                                     Host = mailServer
                                 };

            try
            {
                smtpClient.Send(message);
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                foreach (Attachment item in message.Attachments)
                {
                     item.Dispose();
                }
                message.Attachments.Clear();
            }
            return true;
        }
    }
}
