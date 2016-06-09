namespace orez.ostring {
	class oParams {
		
		/// <summary>
		/// Tells the string function to be used.
		/// </summary>
		public string fn = "";
		/// <summary>
		/// Defines input string to specified function.
		/// </summary>
		public string input = "";
		/// <summary>
		/// Defines input arguments to specified function.
		/// </summary>
		public string[] args = new string[0];
		/// <summary>
		/// Indicates whether specified search pattern is RegEx.
		/// </summary>
		public bool regex = false;
		/// <summary>
		/// Indicates the format in which parameter strings are escaped.
		/// </summary>
		public string escaped = "";
	}
}
