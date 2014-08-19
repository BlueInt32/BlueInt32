using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueInt32.Core.String
{
	public static class RegexPatterns
	{
		/// <summary>
		/// Cette expression regulière est tirée de la librairie d'Expresso, un soft qu'il est bien pour les Regex!
		/// </summary>
		public const string MAIL_PATTERN = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})$";
		
		public const string PEOPLE_NAMES_PATTERN = @"^(\p{L}| |'|\-)+$";
		public const string ADDRESS_ROAD_NUMBER_PATTERN = @"^[0-9]+( )?(bis|ter)?$";
		public const string ADDRESS_CITY_NAME_PATTERN = PEOPLE_NAMES_PATTERN;
		public const string ADDRESS_WAY_NAME_PATTERN = PEOPLE_NAMES_PATTERN;
		public const string ADDRESS_WAY_TYPE_PATTERN = PEOPLE_NAMES_PATTERN;

	}
}
