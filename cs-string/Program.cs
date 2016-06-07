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

		private static string At(string s, int a, int b) {
			a = a > 0 ? a : 0;
			a = a <= s.Length ? a : s.Length;
			b = b > 0 ? b : 0;
			b = b <= s.Length ? b : s.Length;
			bool rev = a > b;
			s = s.Substring(rev? b : a, Math.Abs(b - a));
			return rev ? Reverse(s) : s;
		}

		private static string Compare(string s, string t) {
			return s.CompareTo(t).ToString();
		}

		/// <summary>
		/// Reverse a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <returns>Reversed string.</returns>
		private static string Reverse(string s) {
			char[] c = s.ToCharArray();
			Array.Reverse(c);
			return new string(c);
		}

		/// <summary>
		/// Get the size of a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <returns>Size of string.</returns>
		private static string Size(string s) {
			return s.Length.ToString();
		}
	}
}
