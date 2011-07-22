// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextHelper.cs" company="Itransition">
//   Itransition (c) Copyright. All right reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Text;

namespace Framework.MVC.Helpers
{
    /// <summary>
    ///  Provides helper functionality for strings.
    /// </summary>
    public class TextHelper
    {
        /// <summary>
        /// The space symbol.
        /// </summary>
        public const String SpaceString = " ";

        /// <summary>
        /// Inserts the delimiter before capital letters.
        /// </summary>
        /// <param name="inputText">The input text.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>Returns string splitted by delimiter.</returns>
        public static String InsertDelimiterBeforeCapitalLetters(String inputText, String delimiter)
        {
            if (String.IsNullOrEmpty(inputText))
            {
                return String.Empty;
            }

            if (String.IsNullOrEmpty(delimiter))
            {
                return inputText;
            }

            var stringBuilder = new StringBuilder(inputText);

            int inputTextLength = inputText.Length;
            int delimiterLength = delimiter.Length;
            for (int currentPosition = 0; currentPosition < inputTextLength; currentPosition++)
            {
                char currentCharacter = stringBuilder[currentPosition];
                bool insertDelimeter = Char.IsUpper(currentCharacter) && currentPosition > 0;
                if (insertDelimeter && currentPosition > 0)
                {
                    char prevCharacter = stringBuilder[currentPosition - 1];
                    insertDelimeter &= !Char.IsUpper(prevCharacter);
                }
                if (insertDelimeter)
                {
                    stringBuilder.Insert(currentPosition, delimiter);
                    inputTextLength += delimiterLength;
                    currentPosition += delimiterLength;
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Inserts the space before capital letters.
        /// </summary>
        /// <param name="inputText">The input text.</param>
        /// <returns>>Returns string splitted by space .</returns>
        public static String InsertSpaceBeforeCapitalLetters(String inputText)
        {
            return InsertDelimiterBeforeCapitalLetters(inputText, SpaceString);
        }
    }
}
