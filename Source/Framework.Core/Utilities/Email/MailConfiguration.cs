using System;
using System.Net.Configuration;
using Framework.Core.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace Framework.Core.Utilities.Email
{
    /// <summary>
    /// Summary for MailConfiguration.
    /// </summary>
    public class MailConfiguration
    {
        #region Fields

        private static MailConfiguration current;
        private String smtpServer = String.Empty;
        private String smtpUser = String.Empty;
        private String smtpPassword = String.Empty;
        private String fromEmail = String.Empty;
        private int smtpPort;
   
        #endregion

        #region Properties

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        /// <value>The current.</value>
        public static MailConfiguration Current
        {
            get { return current ?? (current = Read()); }
        }

        /// <summary>
        /// Gets an SMTP server address that should be used within application.
        /// </summary>
        public String SmtpServer
        {
            get { return smtpServer; }
        }

        /// <summary>
        /// Gets an SMTP server login that should be used within application.
        /// </summary>
        public String SmtpUser
        {
            get { return smtpUser; }
        }

        /// <summary>
        /// Gets an SMTP server password that should be used within application.
        /// </summary>
        public String SmtpPassword
        {
            get { return smtpPassword; }
        }

        /// <summary>
        /// Gets or sets the SMTP port.
        /// </summary>
        /// <value>The SMTP port.</value>
        public int SmtpPort
        {
            get { return smtpPort; }
        }

        /// <summary>
        /// Gets or sets from email.
        /// </summary>
        /// <value>From email.</value>
        public String FromEmail
        {
            get { return fromEmail; }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Reads the mail settings from configuration file.
        /// </summary>
        /// <returns></returns>
        private static MailConfiguration Read()
        {
            var mailConfiguration = new MailConfiguration();
            var configurationManager = ServiceLocator.Current.GetInstance<IConfigurationManager>();

            if (configurationManager != null)
            {
                var mailSettings = configurationManager.GetSection<SmtpSection>("system.net/mailSettings/smtp");

                if (mailSettings != null)
                {
                    mailConfiguration.smtpServer = mailSettings.Network.Host;
                    mailConfiguration.smtpUser = mailSettings.Network.UserName;
                    mailConfiguration.smtpPassword = mailSettings.Network.Password;
                    mailConfiguration.smtpPort = mailSettings.Network.Port;
                    mailConfiguration.fromEmail = mailSettings.From;
                }
            }

            return mailConfiguration;
        }

        #endregion
    }
}
