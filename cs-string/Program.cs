using System;

namespace orez.ostring {
	class Program {

		/// <summary>
		/// Smack that. Oooo.
		/// </summary>
		/// <param name="args">Input parameters.</param>
		static void Main(string[] args) {
			string s = "abcd";
			s = s.Substring(3, 0);
		}

		private static string Run(string s, string[] p) {
			if(p.Length > 0) switch(p[0]) {
					case "at":
						return null;
					case "compare":
						return s.CompareTo(p.Length > 1 ? p[1] : "").ToString();
					case "size":
						return s.Length.ToString();
			}
			return null;
		}

		private static string Reverse(string s) {
			char[] c = s.ToCharArray();
			Array.Reverse(c);
			return new string(c);
		}
	}
}
