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
		
		/// <summary>
		/// Add a string to input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">add, index.</param>
		/// <returns>Added string.</returns>
		private static string Add(string s, string[] p) {
			int i = s.Length;
			string t = p.Length > 0 ? p[0] : "";
			if(p.Length > 1) int.TryParse(p[1], out i);
			i = i > s.Length ? s.Length : i;
			while(i < 0) i += s.Length;
			return s.Insert(i, t);
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

		/// <summary>
		/// Compare two strings.
		/// </summary>
		/// <param name="s">1st input string.</param>
		/// <param name="t">2nd input string.</param>
		/// <returns>
		/// A number that is:
		/// &gt; 0 - if 1st string comes after 2nd string.
		///   == 0 - if both the strings are equal.
		/// &lt; 0 - if 1st string comes before 2nd string.
		/// </returns>
		private static string Compare(string s, string[] p) {
			return s.CompareTo(p.Length > 0 ? p[0] : "").ToString();
		}

		/// <summary>
		/// Check whether input string ends with suffix.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">suffix.</param>
		/// <returns>1 if true, 0 otherwise.</returns>
		private static string EndsWith(string s, string[] p) {
			string t = p.Length > 0 ? p[0] : "";
			return s.EndsWith(t) ? "1" : "0";
		}

		/// <summary>
		/// Find index of string in the input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, direction.</param>
		/// <returns></returns>
		private static string Find(string s, string[] p) {
			int i = 0, d = 1;
			string t = p.Length > 0 ? p[0] : "";
			if(p.Length > 1) int.TryParse(p[1], out i);
			if(p.Length > 2) int.TryParse(p[2], out d);
			i = i > s.Length ? s.Length : i;
			while(i < 0) i += s.Length;
			return (d >= 0 ? s.IndexOf(t, i) : s.LastIndexOf(t, i)).ToString();
		}

		/// <summary>
		/// Remove part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">length, index.</param>
		/// <returns>Removed string.</returns>
		private static string Remove(string s, string[] p) {
			int l = 0;
			if(p.Length > 0) int.TryParse(p[0], out l);
			int i = l > s.Length ? 0 : s.Length - l;
			if(p.Length > 1) int.TryParse(p[1], out i);
			i = i > s.Length ? s.Length : i;
			while(i < 0) i += s.Length;
			return s.Substring(0, i - 0) + s.Substring(i + l);
		}

		/// <summary>
		/// Reverse a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Reversed string.</returns>
		private static string Reverse(string s, string[] p) {
			char[] c = s.ToCharArray();
			Array.Reverse(c);
			return new string(c);
		}

		/// <summary>
		/// Get the size of a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Size of string.</returns>
		private static string Size(string s, string[] p) {
			return s.Length.ToString();
		}
	}
}
