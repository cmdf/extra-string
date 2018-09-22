using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;

namespace App.text {

	/// <summary>
	/// Defines string extension methods.
	/// </summary>
	static class StringExt {

		// data
		/// <summary>
		/// String Encode table for DOS without delayed expansion enabled.
		/// </summary>
		private static IDictionary<string, string> EncDos = new Dictionary<string, string> {
			["^"] = "^^",	["\\"] = "^\\", ["&"] = "^&", ["|"] = "^|", [">"] = "^>", ["<"] = "^<", ["%"] = "%%", [""] = "^\r\n"
		};
		/// <summary>
		/// String Encode table for DOS with delayed expansion enabled.
		/// </summary>
		private static IDictionary<string, string> EncDose = new Dictionary<string, string> {
			["^"] = "^^", ["\\"] = "^\\", ["&"] = "^&", ["|"] = "^|", [">"] = "^>", ["<"] = "^<", ["%"] = "%%", [""] = "^\r\n",
			["!"] = "^^!"
		};
		/// <summary>
		/// String Encode table for standard coding language.
		/// </summary>
		private static IDictionary<string, string> EncCode = new Dictionary<string, string> {
			["\""] = "\\\"", ["\\"] = "\\\\", ["\a"] = "\\a", ["\b"] = "\\b", ["\f"] = "\\f", ["\n"] = "\\n", ["\r"] = "\\r",
			["\t"] = "\\t", ["\v"] = "\\v", ["\0"] = "\\0"
		};
		/// <summary>
		/// Regex options associated with characters.
		/// </summary>
		private static IDictionary<char, RegexOptions> ReOpt = new Dictionary<char, RegexOptions> {
			['i'] = RegexOptions.IgnoreCase, ['m'] = RegexOptions.Multiline, ['r'] = RegexOptions.RightToLeft,
			['s'] = RegexOptions.Singleline
		};


		// extension methods
		/// <summary>
		/// Encode or Escape string to coded form.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="typ">Encoding type.</param>
		/// <returns></returns>
		public static string Encode(this string s, EncType typ) {
			switch (typ) {
				case EncType.Html:
					return HttpUtility.HtmlEncode(s);
				case EncType.Url:
					return HttpUtility.UrlEncode(s);
				case EncType.Dos:
					return s.Replace(EncDos, true);
				case EncType.Dose:
					return s.Replace(EncDose, true);
				case EncType.Regex:
					return Regex.Escape(s);
				case EncType.Code:
					return s.Replace(EncCode, true);
			}
			return s;
		}

		/// <summary>
		/// Decode or Unescape string to original form.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="typ">Decoding type.</param>
		/// <returns>Decoded string.</returns>
		public static string Decode(this string s, EncType typ) {
			switch (typ) {
				case EncType.Html:
					return HttpUtility.HtmlDecode(s);
				case EncType.Url:
					return HttpUtility.UrlDecode(s);
				case EncType.Dos:
					return s.Replace(EncDos, false);
				case EncType.Dose:
					return s.Replace(EncDose, false);
				case EncType.Regex:
					return Regex.Unescape(s);
				case EncType.Code:
					return s.Replace(EncCode, false);
			}
			return s;
		}

		/// <summary>
		/// Replace string with associated values from dictionary.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="d">Dictionary used to replace.</param>
		/// <param name="ksrch">Indicates whether to use key as search string, if false use value.</param>
		/// <returns></returns>
		public static string Replace(this string s, IDictionary<string, string> d, bool ksrch = true) {
			if (ksrch) foreach (var p in d) { if (p.Key != "") s = s.Replace(p.Key, p.Value); }
			else foreach (var p in d) { if (p.Value != "") s = s.Replace(p.Value, p.Key); }
			return s;
		}

		/// <summary>
		/// Get Regex from regex string.
		/// </summary>
		/// <param name="s">Regex string.</param>
		/// <returns>Regex object.</returns>
		public static Regex RegEx(this string s, RegexOptions op = RegexOptions.None) {
			s = s.StartsWith("/") ? s.Substring(1) : s;
			int i = s.LastIndexOf('/');
			op |= RegExOpt(i >= 0 ? s.Substring(i + 1) : "");
			s = i >= 0 ? s.Substring(0, i) : s;
			return new Regex(s, op);
		}

		/// <summary>
		/// Get Regex options from options string.
		/// </summary>
		/// <param name="s">Options string.</param>
		/// <returns>Regex options.</returns>
		public static RegexOptions RegExOpt(this string s) {
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

		/// <summary>
		/// Reverse a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <returns>Reversed string.</returns>
		public static string Reverse(this string s) {
			char[] c = s.ToCharArray();
			Array.Reverse(c);
			return new string(c);
		}
	}
}
