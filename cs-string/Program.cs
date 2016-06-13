using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using orez.ostring.data;
using orez.ostring.text;

namespace orez.ostring {

	/// <summary>
	/// Please take me to the party...
	/// </summary>
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
			["size"] = new Fn(Size), ["get"] = new Fn(Get), ["range"] = new Fn(Range),
			["find"] = new Fn(Find), ["compare"] = new Fn(Compare), ["startswith"] = new Fn(StartsWith), ["endswith"] = new Fn(EndsWith),
			["code"] = new Fn(Code), ["encode"] = new Fn(Encode), ["decode"] = new Fn(Decode), ["line"] = new Fn(Line),
			["copy"] = new Fn(Copy), ["format"] = new Fn(Format), ["pad"] = new Fn(Pad), ["trim"] = new Fn(Trim),
			["add"] = new Fn(Add), ["put"] = new Fn(Put),	["replace"] = new Fn(Replace), ["remove"] = new Fn(Remove), ["reverse"] = new Fn(Reverse),
			["uppercase"] = new Fn(UpperCase), ["lowercase"] = new Fn(LowerCase)
		};
		/// <summary>
		/// Associates encoding name with encoding type enum.
		/// </summary>
		private static IDictionary<string, oEncType> EncTyp = new Dictionary<string, oEncType> {
			["html"] = oEncType.Html, ["h"] = oEncType.Html, ["url"] = oEncType.Url, ["u"] = oEncType.Url,
			["dos"] = oEncType.Dos, ["d"] = oEncType.Dos, ["dose"] = oEncType.Dose, ["e"] = oEncType.Dose,
			["regex"] = oEncType.Regex, ["r"] = oEncType.Regex, ["code"] = oEncType.Code, ["c"] = oEncType.Code
		};

		// methods
		/// <summary>
		/// Smack that. Oooo.
		/// </summary>
		/// <param name="args">Input parameters.</param>
		static void Main(string[] args) {
			oParams p = GetOpt(args);
			if(StrFn.ContainsKey(p.fn))	StrFn[p.fn](p.input, p.args, p.regex);
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
					case "--encoded":
					case "-e":
						p.encoded = args[++i];
						break;
					case "--input":
					case "-i":
						p.input = args[++i];
						break;
					default:
						if (p.input == null) p.input = new StreamReader(Console.OpenStandardInput()).ReadToEnd();
						p.fn = args[i++].ToLower();
						p.args = new string[args.Length - i];
						for(int d=0; i<args.Length; d++, i++)
							p.args[d] = args[i].Decode(EncTyp.GetDef(p.encoded));
						break;
				}
			}
			return p;
		}

		/// <summary>
		/// Get the size of a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <param name="re">NA.</param>
		private static void Size(string s, string[] p, bool re) {
			Console.WriteLine(s.Length);
		}
		/// <summary>
		/// Get a part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">index, length.</param>
		/// <param name="re">NA.</param>
		private static void Get(string s, string[] p, bool re) {
			int i = Indx(Int(p, 0), s);
			int e = Indx(i + Int(p, 1, 1), s);
			Print(s.Substring(i, e - i));
		}
		/// <summary>
		/// Get a specified range of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, end.</param>
		/// <param name="re">NA.</param>
		private static void Range(string s, string[] p, bool re) {
			int i = Indx(Int(p, 0), s);
			int e = Indx(Int(p, 1, s.Length), s);
			Print(s.Substring(i, e - i));
		}
		/// <summary>
		/// Find index of string in the input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">start, direction.</param>
		/// <param name="re">Is search string regex?</param>
		private static void Find(string s, string[] p, bool re) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1), s), d = Int(p, 1, 1);
			if (!re) for (int n = 0, N = Math.Abs(d); n < N; n++, i = d >= 0 ? i + 1 : i - 1)
					Console.WriteLine(i = (d >= 0 ? s.IndexOf(t, i) : s.LastIndexOf(t, i)));
			foreach (Match m in t.RegEx(d < 0 ? RegexOptions.RightToLeft : RegexOptions.None).Matches(s, i))
				Console.WriteLine(m.Index + " " + m.Length);
		}
		/// <summary>
		/// Compare two strings.
		/// Returns a number that is:
		/// &gt; 0 - if 1st string comes after 2nd string.
		///   == 0 - if both the strings are equal.
		/// &lt; 0 - if 1st string comes before 2nd string.
		/// </summary>
		/// <param name="s">1st input string.</param>
		/// <param name="t">2nd input string.</param>
		/// <param name="re">Is search string regex?</param>
		private static void Compare(string s, string[] p, bool re) {
			string t = Str(p, 0);
			if (!re) { Console.WriteLine(s.CompareTo(t)); return; }
			Console.WriteLine(t.RegEx().Match(s).Length == s.Length ? "0" : "-1");
		}
		/// <summary>
		/// Check whether input string starts with prefix.
		/// Returns 1 if true, 0 otherwise.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">prefix.</param>
		/// <param name="re">Is search string regex?</param>
		private static void StartsWith(string s, string[] p, bool re) {
			string t = Str(p, 0);
			if (!re) Console.WriteLine(s.StartsWith(t) ? "1" : "0");
			Match m = t.RegEx().Match(s);
			Console.WriteLine(m.Length == t.Length ? "1" : "0");
		}
		/// <summary>
		/// Check whether input string ends with suffix.
		/// Returns 1 if true, 0 otherwise.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">suffix.</param>
		/// <param name="re">Is search string regex?</param>
		private static void EndsWith(string s, string[] p, bool re) {
			string t = Str(p, 0);
			if (!re) Console.WriteLine(s.EndsWith(t) ? "1" : "0");
			Match m = t.RegEx(RegexOptions.RightToLeft).Match(s);
			Console.WriteLine(m.Length == t.Length ? "1" : "0");
		}
		/// <summary>
		/// Get character codes for each character in the string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <param name="re">Is search string regex?</param>
		private static void Code(string s, string[] p, bool re) {
			for (int i = 0; i < s.Length; i++)
				Console.WriteLine((uint)s[i]);
		}
		/// <summary>
		/// Encode or Escape string to coded form.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">type.</param>
		/// <param name="re">NA.</param>
		private static void Encode(string s, string[] p, bool re) {
			oEncType t = EncTyp.GetDef(Str(p, 0));
			Print(s.Encode(t));
		}
		/// <summary>
		/// Decode or Unescape string to original form.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">type.</param>
		/// <param name="re">NA.</param>
		private static void Decode(string s, string[] p, bool re) {
			oEncType t = EncTyp.GetDef(Str(p, 0));
			Print(s.Decode(t));
		}
		/// <summary>
		/// Replace line endings in input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">ending.</param>
		/// <param name="re">NA.</param>
		private static void Line(string s, string[] p, bool re) {
			string t = Str(p, 0, "\r\n");
			Print(Regex.Replace(s, "(\r\n)|(\r)|(\n)", t));
		}
		/// <summary>
		/// Copies input string specified number of times.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">times.</param>
		/// <param name="re">NA.</param>
		private static void Copy(string s, string[] p, bool re) {
			StringBuilder t = new StringBuilder();
			int n = Math.Abs(Int(p, 0));
			Print(s.Repeat(n));
		}
		/// <summary>
		/// Uses input string as format to embed parameter strings.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">parameters.</param>
		/// <param name="re">NA.</param>
		private static void Format(string s, string[] p, bool re) {
			Print(string.Format(s, p));
		}
		/// <summary>
		/// Pad input string on the left and/or right.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">times, dir, pad.</param>
		/// <param name="re">NA.</param>
		private static void Pad(string s, string[] p, bool re) {
			int n = Int(p, 0, 1), d = Int(p, 1);
			string t = Str(p, 2, " ");
			string r = t.Repeat(n);
			Print((d <= 0 ? r : "") + s + (d >= 0 ? r : ""));
		}
		/// <summary>
		/// Trim input string on the left and/or right.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">dir, trim.</param>
		/// <param name="re">NA.</param>
		private static void Trim(string s, string[] p, bool re) {
			int d = Int(p, 0);
			char[] t = Str(p, 1, " \t\r\n").ToCharArray();
			if (t.Length == 0) Print(s);
			else if (d < 0) Print(s.TrimStart(t));
			else if (d > 0) Print(s.TrimEnd(t));
			else Print(s.Trim(t));
		}
		/// <summary>
		/// Add a string to input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">add, index.</param>
		/// <param name="re">NA.</param>
		private static void Add(string s, string[] p, bool re) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1, s.Length), s);
			Print(s.Insert(i, t));
		}
		/// <summary>
		/// Put a string onto input string at specified index.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">string, index.</param>
		/// <param name="re">NA.</param>
		private static void Put(string s, string[] p, bool re) {
			string t = Str(p, 0);
			int i = Indx(Int(p, 1), s);
			int e = Indx(i + t.Length, s);
			Print(s.Remove(i, e - i).Insert(i, t));
		}
		/// <summary>
		/// Replace a search string with new string in input string.
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="p">search, new.</param>
		/// <param name="re">Is search string regex?</param>
		private static void Replace(string s, string[] p, bool re) {
			string t = Str(p, 0), u = Str(p, 1);
			if (t == "") Print(string.Join(u, t.ToCharArray()));
			else if (!re) Print(s.Replace(t, u));
			else Print(t.RegEx().Replace(s, u));
		}
		/// <summary>
		/// Remove part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">length, index.</param>
		/// <param name="re">NA.</param>
		private static void Remove(string s, string[] p, bool re) {
			int l = Indx(Int(p, 0), s);
			int i = Indx(Int(p, 1, s.Length - l), s);
			Print(s.Remove(i, l));
		}
		/// <summary>
		/// Reverse a string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <param name="re">NA.</param>
		private static void Reverse(string s, string[] p, bool re) {
			Print(s.Reverse());
		}
		/// <summary>
		/// Convert input string to lower case.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <param name="re">NA.</param>
		private static void LowerCase(string s, string[] p, bool re) {
			Print(s.ToLower());
		}
		/// <summary>
		/// Convert input string to upper case.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">NA.</param>
		/// <param name="re">NA.</param>
		private static void UpperCase(string s, string[] p, bool re) {
			Print(s.ToUpper());
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
		/// <summary>
		/// Print a line normally on console, but add no line ending in redirected output.
		/// </summary>
		/// <param name="o">Input object.</param>
		private static void Print(object o) {
			if (Console.IsOutputRedirected) Console.Write(o);
			else Console.WriteLine(o);
		}
	}
}
