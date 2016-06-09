using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace orez.ostring {
	class Program {

		// type
		public delegate string Fn(string s, string[] p);

		// data
		private static IDictionary<string, string> DefEsc = new Dictionary<string, string> {
			{ "\"", "\\\"" }, { "\\", "\\\\" }, { "\a", "\\a" }, {"\b", "\\b" }, {"\f", "\\f" },
			{"\n", "\\n" }, {"\r", "\\r" }, {"\t", "\\t" }, {"\v", "\\v" }, {"\0", "\\0" }
		};
		private static IDictionary<string, Fn> Cmd = new Dictionary<string, Fn> {
			["add"] = new Fn(Add), ["compare"] = new Fn(Compare), ["endswith"] = new Fn(EndsWith),
			["find"] = new Fn(Find), ["remove"] = new Fn(Remove), ["replace"] = new Fn(Replace),
			["reverse"] = new Fn(Reverse), ["size"] = new Fn(Size)
		};

		/// <summary>
		/// Smack that. Oooo.
		/// </summary>
		/// <param name="args">Input parameters.</param>
		static void Main(string[] args) {
			string s = new StreamReader(Console.OpenStandardInput()).ReadToEnd();
			oParams p = GetOpt(args);
			Console.WriteLine(Cmd[p.fn](s, p.args));
		}

		/// <summary>
		/// Get input options to ostring.
		/// </summary>
		/// <param name="args">Input arguments.</param>
		/// <returns>Input options.</returns>
		private static oParams GetOpt(string[] args) {
			oParams p = new oParams();
			for(int i = 0; i < args.Length; i++) {
				switch(args[i]) {
					case "--regex":
					case "-r":
						p.regex = true;
						break;
					default:
						p.fn = args[i++].ToLower();
						p.args = new string[args.Length - i];
						Array.Copy(args, i, p.args, 0, p.args.Length);
						i = args.Length;
						break;
				}
			}
			return p;
		}

		/// <summary>
		/// Add a string to input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">add, index.</param>
		/// <returns>Added string.</returns>
		private static string Add(string s, string[] p) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1, s.Length), s);
			return s.Insert(i, t);
		}
		/// <summary>
		/// Get a part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">index, length.</param>
		/// <returns>Part of string.</returns>
		private static string Get(string s, string[] p) {
			int i = Indx(Int(p, 0), s);
			int e = Indx(i + Int(p, 1, 1), s);
			return s.Substring(i, e - i);
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
			string t = Str(p, 0);
			return s.CompareTo(t).ToString();
		}
		/// <summary>
		/// Copies input string specified number of times.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">times.</param>
		/// <returns>Copied string.</returns>
		private static string Copy(string s, string[] p) {
			string t = "";
			int n = Math.Abs(Int(p, 0));
      for(int i = 0; i < n; i++)
				t += s;
			return t;
		}
		/// <summary>
		/// Check whether input string ends with suffix.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">suffix.</param>
		/// <returns>1 if true, 0 otherwise.</returns>
		private static string EndsWith(string s, string[] p) {
			string t = Str(p, 0);
			return s.EndsWith(t) ? "1" : "0";
		}

		private static string Escape(string s, string[] p) {
			string t = Str(p, 0);
			switch(t.ToLower()) {
				case "regex":
				case "r":
					return Regex.Escape(s);
				default:
					break;
			}
			return null;
		}

		/// <summary>
		/// Find index of string in the input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, direction.</param>
		/// <returns></returns>
		private static string Find(string s, string[] p) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1), s), d = Int(p, 1);
			return (d >= 0 ? s.IndexOf(t, i) : s.LastIndexOf(t, i)).ToString();
		}
		/// <summary>
		/// Uses input string as format to embed parameter strings.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">parameters.</param>
		/// <returns>Formatted string.</returns>
		private static string Format(string s, string[] p) {
			return string.Format(s, p);
		}
		/// <summary>
		/// Put a string onto input string at specified index.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">string, index.</param>
		/// <returns>Put string.</returns>
		private static string Put(string s, string[] p) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1), s);
			int e = Indx(i + t.Length, s);
			return s.Remove(i, e - i).Insert(i, t);
		}
		/// <summary>
		/// Convert input string to lower case.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Lower cased string.</returns>
		private static string LowerCase(string s, string[] p) {
			return s.ToLower();
		}
		/// <summary>
		/// Get a specified range of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, end.</param>
		/// <returns>Part of string.</returns>
		private static string Range(string s, string[] p) {
			int i = Indx(Int(p, 0), s);
			int e = Indx(Int(p, 1, s.Length), s);
			return s.Substring(i, e - i);
		}
		/// <summary>
		/// Remove part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">length, index.</param>
		/// <returns>Removed string.</returns>
		private static string Remove(string s, string[] p) {
			int l = Indx(Int(p, 0), s);
			int i = Indx(Int(p, 1, s.Length - l), s);
			return s.Remove(i, l);
		}
		/// <summary>
		/// Replace a search string with new string in input string.
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="p">search, new.</param>
		/// <returns>Replaced string.</returns>
		private static string Replace(string s, string[] p) {
			string t = Str(p, 0), u = Str(p, 1);
			return t == "" ? string.Join(u, t.ToCharArray()) : s.Replace(t, u);
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
		/// <summary>
		/// Check whether input string starts with prefix.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">prefix.</param>
		/// <returns>1 if true, 0 otherwise.</returns>
		private static string StartsWith(string s, string[] p) {
			string t = Str(p, 0);
			return s.StartsWith(t) ? "1" : "0";
		}
		// TODO: can we make this one function for all?
		/// <summary>
		/// Convert input string to unix line ending.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Unix lined string.</returns>
		private static string LfLine(string s, string[] p) {
			return s.Replace("\r\n", "\n").Replace('\r', '\n');
		}
		/// <summary>
		/// Convert input string to upper case.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Upper cased string.</returns>
		private static string UpperCase(string s, string[] p) {
			return s.ToUpper();
		}

		/// <summary>
		/// Get ranged index for specified string.
		/// </summary>
		/// <param name="s">String value.</param>
		/// <param name="i">Index in string.</param>
		/// <returns>Ranged index.</returns>
		private static int Indx(int i, string s) {
			i = i > s.Length ? s.Length : i;
			while(i < 0) i += s.Length;
			return i;
		}
		/// <summary>
		/// Get string from specified index of string array.
		/// </summary>
		/// <param name="a">String array.</param>
		/// <param name="i">Array index.</param>
		/// <param name="v">Optional. Default value.</param>
		/// <returns>String value at specified index, or default value.</returns>
		private static string Str(string[] a, int i, string v="") {
			return a.Length > i ? a[i] : v;
		}
		/// <summary>
		/// Get int from specified index of string array.
		/// </summary>
		/// <param name="a">String array.</param>
		/// <param name="i">Array index.</param>
		/// <param name="v">Optional. Default value.</param>
		/// <returns>Int value at specified index, or default value.</returns>
		private static int Int(string[] a, int i, int v=0) {
			if(a.Length > i) int.TryParse(a[i], out v);
			return v;
		}

		// 1 trim, 1 pad, ascii -> generic encodig format?
		// isint isdecimal -> can be implemented with regex equals
		// line -> generic line find and replace?
		// multi-line operate, input stream operate
		// --regex|-r flag for regex search pattern
	}
}
