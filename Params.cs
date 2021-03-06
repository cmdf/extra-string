﻿namespace App {

	/// <summary>
	/// Defines input parameters to estring.
	/// </summary>
	class Params {
		
		/// <summary>
		/// Tells the string function to be used.
		/// </summary>
		public string fn = "";
		/// <summary>
		/// Defines input string to specified function.
		/// </summary>
		public string input = null;
		/// <summary>
		/// Defines input arguments to specified function.
		/// </summary>
		public string[] args = new string[0];
		/// <summary>
		/// Indicates whether specified search pattern is RegEx.
		/// </summary>
		public bool regex = false;
		/// <summary>
		/// Indicates the format in which parameter strings are encoded.
		/// </summary>
		public string encoded = "";
	}
}
