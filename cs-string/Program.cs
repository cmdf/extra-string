using System;

namespace orez.ostring {
	class Program {

		/// <summary>
		/// Smack that. Oooo.
		/// </summary>
		/// <param name="args">Input parameters.</param>
		static void Main(string[] args) {
		}

		private static string Run(string s, string[] p) {
			if(p.Length > 0) switch(p[0]) {
					case "size":
						return s.Length.ToString();
			}
			return null;
		}
	}
}
