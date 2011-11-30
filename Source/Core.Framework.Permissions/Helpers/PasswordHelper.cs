using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Core.Framework.Permissions.Models;

namespace Core.Framework.Permissions.Helpers
{
    /// <summary>
    /// Provides helper methods for password encryption and veryfication.
    /// </summary>
    public static class PasswordHelper
    {
        #region Fields

        private const int SaltLength = 32;

        private static readonly Dictionary<PasswordMode, HashAlgorithm> hashAlgorithmMapping = new Dictionary<PasswordMode, HashAlgorithm>
        {
            { PasswordMode.MD5, new MD5CryptoServiceProvider() },
            { PasswordMode.SHA1, new SHA1Managed() },
            { PasswordMode.SHA256, new SHA256Managed() },
            { PasswordMode.SHA384, new SHA384Managed() },
            { PasswordMode.SHA512, new SHA512Managed() },
        };

        #endregion

        #region Methods

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// verification.
        /// </summary>
        /// <param name="plainText">Plaintext value to be hashed. The function does not check whether this parameter is null.</param>
        /// <param name="mode">Password encryption mode.</param>
        /// <returns>
        /// Password hash and salt.
        /// </returns>
        public static PasswordHash Encrypt(String plainText, PasswordMode mode)
        {
            PasswordHash result;

            if (hashAlgorithmMapping.ContainsKey(mode))
            {
                HashAlgorithm algorithm = hashAlgorithmMapping[mode];

                var saltBytes = new byte[SaltLength];
                RandomNumberGenerator.Create().GetBytes(saltBytes);
                var passwordBytes = Encoding.UTF8.GetBytes(plainText);
                var hashBytes = Encrypt(passwordBytes, saltBytes, algorithm);

                result = new PasswordHash
                {
                    Salt = Convert.ToBase64String(saltBytes),
                    Hash = Convert.ToBase64String(hashBytes)
                };
            }
            else
            {
                result = new PasswordHash
                {
                    Salt = String.Empty,
                    Hash = plainText
                };
            }

            return result;
        }

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// verification.
        /// </summary>
        /// <param name="plainText">Plaintext value to be hashed. The function does not check whether this parameter is null.</param>
        /// <param name="mode">Password encryption mode.</param>
        /// <param name="salt">Random bits that are used as one of the inputs to a key derivation function (formatted as a base64-encoded string).</param>
        /// <returns>Password hash and salt.</returns>
        public static PasswordHash Encrypt(String plainText, PasswordMode mode, String salt)
        {
            PasswordHash result;

            if (hashAlgorithmMapping.ContainsKey(mode))
            {
                HashAlgorithm algorithm = hashAlgorithmMapping[mode];

                var saltBytes = Convert.FromBase64String(salt);
                var passwordBytes = Encoding.UTF8.GetBytes(plainText);
                var hashBytes = Encrypt(passwordBytes, saltBytes, algorithm);

                result = new PasswordHash
                {
                    Salt = salt,
                    Hash = Convert.ToBase64String(hashBytes)
                };
            }
            else
            {
                result = new PasswordHash
                {
                    Salt = String.Empty,
                    Hash = plainText
                };
            }

            return result;
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="plainText">Plain text to be verified against the specified hash. The function does not check whether this parameter is null.</param>
        /// <param name="expected">Password expected hash.</param>
        /// <param name="mode">Password encryption mode.</param>
        /// <returns>
        ///     <c>true</c> if computed hash mathes the specified hash the function; otherwise <c>false</c>.
        /// </returns>
        public static bool Verify(String plainText, PasswordHash expected, PasswordMode mode)
        {
            var hash = Encrypt(plainText, mode, expected.Salt);
            return expected.Equals(hash);
        }

        private static byte[] Encrypt(byte[] passwordBytes, byte[] saltBytes, HashAlgorithm algorithm)
        {
            var buffer = new byte[passwordBytes.Length + saltBytes.Length];
            passwordBytes.CopyTo(buffer, 0);
            saltBytes.CopyTo(buffer, passwordBytes.Length);
            return algorithm.ComputeHash(buffer);
        }

        #endregion
    }
}
