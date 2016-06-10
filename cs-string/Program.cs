using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace orez.ostring {
	class Program {

		// types
		/// <summary>
		/// Defines a string function.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">Input parameters.</param>
		/// <param name="re">Indicates search pattern is regex.</param>
		/// <returns></returns>
		public delegate void Fn(string s, string[] p, bool re);

		// data
		/// <summary>
		/// Associates function name with the actual function.
		/// </summary>
		private static IDictionary<string, Fn> StrFn = new Dictionary<string, Fn> {
			{"add",  new Fn(Add)}, {"get", new Fn(Get)}, {"compare", new Fn(Compare)}, {"copy", new Fn(Copy)},
			{ "endswith", new Fn(EndsWith)}, {"find",  new Fn(Find)}, {"format", new Fn(Format)}, {"lowercase", new Fn(LowerCase)},
			{ "put", new Fn(Put)}, {"range", new Fn(Range)}, { "remove", new Fn(Remove)}, { "replace", new Fn(Replace)},
			{"reverse", new Fn(Reverse)}, {"size", new Fn(Size)}, {"startswith", new Fn(StartsWith)}, {"uppercase", new Fn(UpperCase)}
		};
		/// <summary>
		/// To be dealt with later.
		/// </summary>
		private static IDictionary<string, string> DefEsc = new Dictionary<string, string> {
			{ "\"", "\\\"" }, { "\\", "\\\\" }, { "\a", "\\a" }, {"\b", "\\b" }, {"\f", "\\f" },
			{"\n", "\\n" }, {"\r", "\\r" }, {"\t", "\\t" }, {"\v", "\\v" }, {"\0", "\\0" }
		};
		/// <summary>
		/// Regex options associated with characters.
		/// </summary>
		private static IDictionary<char, RegexOptions> ReOpt = new Dictionary<char, RegexOptions> {
			['i'] = RegexOptions.IgnoreCase, ['m'] = RegexOptions.Multiline, ['r'] = RegexOptions.RightToLeft,
			['s'] = RegexOptions.Singleline
		};


		// methods
		/// <summary>
		/// Smack that. Oooo.
		/// </summary>
		/// <param name="args">Input parameters.</param>
		static void Main(string[] args) {
			string s = new StreamReader(Console.OpenStandardInput()).ReadToEnd();
			oParams p = GetOpt(args);
			if(StrFn.ContainsKey(p.fn))	StrFn[p.fn](s, p.args, p.regex);
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
					case "--escaped":
					case "-e":
						p.escaped = args[++i];
						break;
					case "--input":
					case "-i":
						p.input = new StreamReader(Console.OpenStandardInput()).ReadToEnd();
						break;
					default:
						p.fn = args[i++].ToLower();
						if(p.input == null) p.input = args[i++];
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
		private static void Add(string s, string[] p, bool re) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1, s.Length), s);
			Console.WriteLine(s.Insert(i, t));
		}
		/// <summary>
		/// Get a part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">index, length.</param>
		/// <returns>Part of string.</returns>
		private static void Get(string s, string[] p, bool re) {
			int i = Indx(Int(p, 0), s);
			int e = Indx(i + Int(p, 1, 1), s);
			Console.WriteLine(s.Substring(i, e - i));
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
		private static void Compare(string s, string[] p, bool re) {
			string t = Str(p, 0);
			if(!re) { Console.WriteLine(s.CompareTo(t)); return; }
			Console.WriteLine(RegEx(t).Match(s).Length == s.Length ? "0" : "-1");
		}
		/// <summary>
		/// Copies input string specified number of times.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">times.</param>
		/// <returns>Copied string.</returns>
		private static void Copy(string s, string[] p, bool re) {
			StringBuilder t = new StringBuilder();
			int n = Math.Abs(Int(p, 0));
      for(int i = 0; i < n; i++)
				t.Append(s);
			Console.WriteLine(t);
		}
		/// <summary>
		/// Check whether input string ends with suffix.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">suffix.</param>
		/// <returns>1 if true, 0 otherwise.</returns>
		private static void EndsWith(string s, string[] p, bool re) {
			string t = Str(p, 0);
			if(!re) Console.WriteLine(s.EndsWith(t) ? "1" : "0");
			Match m = RegEx(t, RegexOptions.RightToLeft).Match(s);
			Console.WriteLine(m.Length == t.Length ? "1" : "0");
		}
		/// <summary>
		/// Do some escaping!!!!!!!
		/// </summary>
		/// <param name="s"></param>
		/// <param name="p"></param>
		/// <param name="re"></param>
		/// <returns></returns>
		private static void Escape(string s, string[] p, bool re) {
			string t = Str(p, 0);
			switch(t.ToLower()) {
				case "regex":
				case "r":
					Console.WriteLine(Regex.Escape(s));
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// Find index of string in the input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, direction.</param>
		/// <returns></returns>
		private static void Find(string s, string[] p, bool re) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1), s), d = Int(p, 1, 1);
			if(!re) for(int n = 0, N = Math.Abs(d); n < N; n++, i = d >= 0 ? i + 1 : i - 1)
					Console.WriteLine(i = (d >= 0 ? s.IndexOf(t, i) : s.LastIndexOf(t, i)));
			foreach(Match m in RegEx(t, d < 0 ? RegexOptions.RightToLeft : RegexOptions.None).Matches(s, i))
				Console.WriteLine(m.Index+" "+m.Length);
		}
		/// <summary>
		/// Uses input string as format to embed parameter strings.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">parameters.</param>
		/// <returns>Formatted string.</returns>
		private static void Format(string s, string[] p, bool re) {
			Console.WriteLine(string.Format(s, p));
		}
		/// <summary>
		/// Convert input string to lower case.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Lower cased string.</returns>
		private static void LowerCase(string s, string[] p, bool re) {
			Console.WriteLine(s.ToLower());
		}
		/// <summary>
		/// Put a string onto input string at specified index.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">string, index.</param>
		/// <returns>Put string.</returns>
		private static void Put(string s, string[] p, bool re) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1), s);
			int e = Indx(i + t.Length, s);
			Console.WriteLine(s.Remove(i, e - i).Insert(i, t));
		}
		/// <summary>
		/// Get a specified range of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, end.</param>
		/// <returns>Part of string.</returns>
		private static void Range(string s, string[] p, bool re) {
			int i = Indx(Int(p, 0), s);
			int e = Indx(Int(p, 1, s.Length), s);
			Console.WriteLine(s.Substring(i, e - i));
		}
		/// <summary>
		/// Remove part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">length, index.</param>
		/// <returns>Removed string.</returns>
		private static void Remove(string s, string[] p, bool re) {
			int l = Indx(Int(p, 0), s);
			int i = Indx(Int(p, 1, s.Length - l), s);
			Console.WriteLine(s.Remove(i, l));
		}
		/// <summary>
		/// Replace a search string with new string in input string.
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="p">search, new.</param>
		/// <returns>Replaced string.</returns>
		private static void Replace(string s, string[] p, bool re) {
			string t = Str(p, 0), u = Str(p, 1);
			Console.WriteLine(t == "" ? string.Join(u, t.ToCharArray()) : s.Replace(t, u));
		}
		/// <summary>
		/// Reverse a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Reversed string.</returns>
		private static void Reverse(string s, string[] p, bool re) {
			char[] c = s.ToCharArray();
			Array.Reverse(c);
			Console.WriteLine(new string(c));
		}
		/// <summary>
		/// Get the size of a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Size of string.</returns>
		private static void Size(string s, string[] p, bool re) {
			Console.WriteLine(s.Length);
		}
		/// <summary>
		/// Check whether input string starts with prefix.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">prefix.</param>
		/// <returns>1 if true, 0 otherwise.</returns>
		private static void StartsWith(string s, string[] p, bool re) {
			string t = Str(p, 0);
			if(!re) Console.WriteLine(s.StartsWith(t) ? "1" : "0");
			Match m = RegEx(t).Match(s);
			Console.WriteLine(m.Length == t.Length ? "1" : "0");
		}
		// TODO: can we make this one function for all?
		/// <summary>
		/// Convert input string to unix line ending.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Unix lined string.</returns>
		private static void LfLine(string s, string[] p, bool re) {
			Console.WriteLine(s.Replace("\r\n", "\n").Replace('\r', '\n'));
		}
		/// <summary>
		/// Convert input string to upper case.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <returns>Upper cased string.</returns>
		private static void UpperCase(string s, string[] p, bool re) {
			Console.WriteLine(s.ToUpper());
		}

		/// <summary>
		/// Get Regex from regex string.
		/// </summary>
		/// <param name="s">Regex string.</param>
		/// <returns>Regex object.</returns>
		private static Regex RegEx(string s, RegexOptions op=RegexOptions.None) {
			s = s.StartsWith("/") ? s.Substring(1) : s;
			int i = s.LastIndexOf('/');
			op |= RegExOpt(i >= 0 ? s.Substring(i + 1) : "");
			s = i >= 0 ? s.Substring(0, i - 1) : s;
			return new Regex(s, op);
		}
		/// <summary>
		/// Get Regex options from options string.
		/// </summary>
		/// <param name="s">Options string.</param>
		/// <returns>Regex options.</returns>
		private static RegexOptions RegExOpt(string s) {
			RegexOptions o = RegexOptions.None;
			for(int i = 0; i < s.Length; i++)
				if(ReOpt.ContainsKey(s[i])) o |= ReOpt[s[i]];
			return o;
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
	}
}
