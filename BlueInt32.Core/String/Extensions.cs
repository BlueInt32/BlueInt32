using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueInt32.Core.String
{
    public static class Extensions
    {
        /// <summary>
        /// Transforme une chaine de caractère en int si le format est valide
        /// </summary>
        /// <param name="str">La chaine à convertir</param>
        /// <returns>Un entier si format est valide. Null autrement</returns>
        public static int? ToInt(this string str)
        {
            int i;
            if (string.IsNullOrEmpty(str))
                return null;

            if (int.TryParse(str, out i))
                return i;

            return null;
        }

    }
}
