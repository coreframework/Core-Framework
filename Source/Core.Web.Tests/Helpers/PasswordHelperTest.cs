using Core.Web.NHibernate.Helpers;
using Core.Web.NHibernate.Models;
using NUnit.Framework;

namespace Core.Web.Tests.Helpers
{
    /// <summary>
    /// Test <see cref="PasswordHelper"/> functionality.
    /// </summary>
    [TestFixture]
    public class PasswordHelperTest
    {
        /// <summary>
        /// Password witout encryption test.
        /// </summary>
        [Test]
        public void ClearTextPasswordTest()
        {
            var password = PasswordHelper.Encrypt("password", PasswordMode.ClearText);

            Assert.IsNotNull(password.Hash);
            Assert.IsNullOrEmpty(password.Salt);
            Assert.IsTrue(PasswordHelper.Verify("password", password, PasswordMode.ClearText));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.MD5));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.SHA1));
        }

        /// <summary>
        /// MD5 encryption test.
        /// </summary>
        [Test]
        public void EncryptMD5Test()
        {
            var password = PasswordHelper.Encrypt("password", PasswordMode.MD5);

            Assert.IsNotNull(password.Hash);
            Assert.IsNotNull(password.Salt);
            Assert.IsTrue(PasswordHelper.Verify("password", password, PasswordMode.MD5));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.ClearText));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.SHA1));
        }

        /// <summary>
        /// SHA1 encryption test.
        /// </summary>
        [Test]
        public void EncryptSHA1Test()
        {
            var password = PasswordHelper.Encrypt("password", PasswordMode.SHA1);

            Assert.IsNotNull(password.Hash);
            Assert.IsNotNull(password.Salt);
            Assert.IsTrue(PasswordHelper.Verify("password", password, PasswordMode.SHA1));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.ClearText));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.MD5));
        }

        /// <summary>
        /// SHA256 encryption test.
        /// </summary>
        [Test]
        public void EncryptSHA256Test()
        {
            var password = PasswordHelper.Encrypt("password", PasswordMode.SHA256);

            Assert.IsNotNull(password.Hash);
            Assert.IsNotNull(password.Salt);
            Assert.IsTrue(PasswordHelper.Verify("password", password, PasswordMode.SHA256));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.ClearText));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.MD5));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.SHA1));
        }

        /// <summary>
        /// SHA384 encryption test.
        /// </summary>
        [Test]
        public void EncryptSHA384Test()
        {
            var password = PasswordHelper.Encrypt("password", PasswordMode.SHA384);

            Assert.IsNotNull(password.Hash);
            Assert.IsNotNull(password.Salt);
            Assert.IsTrue(PasswordHelper.Verify("password", password, PasswordMode.SHA384));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.ClearText));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.MD5));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.SHA256));
        }

        /// <summary>
        /// SHA512 encryption test.
        /// </summary>
        [Test]
        public void EncryptSHA512Test()
        {
            var password = PasswordHelper.Encrypt("password", PasswordMode.SHA512);

            Assert.IsNotNull(password.Hash);
            Assert.IsNotNull(password.Salt);
            Assert.IsTrue(PasswordHelper.Verify("password", password, PasswordMode.SHA512));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.ClearText));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.MD5));
            Assert.IsFalse(PasswordHelper.Verify("password", password, PasswordMode.SHA384));
        }
    }
}
