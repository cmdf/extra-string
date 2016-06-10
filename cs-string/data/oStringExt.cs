using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace orez.ostring.data {
	static class oStringExt {

		// data
		/// <summary>
		/// Regex options associated with characters.
		/// </summary>
		private static IDictionary<char, RegexOptions> ReOpt = new Dictionary<char, RegexOptions> {
			['i'] = RegexOptions.IgnoreCase, ['m'] = RegexOptions.Multiline, ['r'] = RegexOptions.RightToLeft,
			['s'] = RegexOptions.Singleline
		};


		// extension methods
		/// <summary>
		/// Get Regex options from options string.
		/// </summary>
		/// <param name="s">Options string.</param>
		/// <returns>Regex options.</returns>
		public static RegexOptions RegExOpt(string s) {
			RegexOptions o = RegexOptions.None;
			for (int i = 0; i < s.Length; i++)
				if (ReOpt.ContainsKey(s[i])) o |= ReOpt[s[i]];
			return o;
		}

		/// <summary>
		/// Repeat a string certain number of times.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="n">Times.</param>
		/// <returns>Repeated string.</returns>
		public static string Repeat(this string s, int n) {
			StringBuilder o = new StringBuilder();
			for (int i = 0; i < n; i++)
				o.Append(s);
			return o.ToString();
		}
	}
}
