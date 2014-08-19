using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueInt32.Core.String.Validation
{
    public static class Extensions
    {
        
        /// <summary>
        /// teste une chaine de caractère pour savoir si elle représente une adresse mail valide
        /// </summary>
        /// <param name="str">La chaine à tester</param>
        /// <returns>True si la chaine est valide. False autrement</returns>
        public static bool IsValidMail(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

			return Regex.IsMatch(str, RegexPatterns.MAIL_PATTERN);
        }

		public static bool IsValidName(this string str)
		{
			return Regex.IsMatch(str, RegexPatterns.PEOPLE_NAMES_PATTERN);
		}

    }
}
