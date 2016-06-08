using System;
using System.IO;
using System.Collections.Generic;

namespace orez.ostring {
	class Program {

		// type
		public delegate string Fn(string s, string[] p);

		// data
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
			string[] p = new string[args.Length - 1];
			Array.Copy(args, 1, p, 0, p.Length);
			Console.WriteLine(Cmd[args[0].ToLower()](s, p));
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

		/// <summary>
		/// Get a part of input string.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">index, length.</param>
		/// <returns>Part of string.</returns>
		private static string Get(string s, string[] p) {
			int i = 0, l = 1;
			if(p.Length > 0) int.TryParse(p[0], out i);
			if(p.Length > 1) int.TryParse(p[1], out l);
			i = i > s.Length ? s.Length : i;
			while(i < 0) i += s.Length;
			int e = i + l;
			e = e > s.Length ? s.Length : e;
			while(e < 0) e += s.Length;
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
			return s.CompareTo(p.Length > 0 ? p[0] : "").ToString();
		}

		/// <summary>
		/// Copies input string specified number of times.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">times.</param>
		/// <returns>Copied string.</returns>
		private static string Copy(string s, string[] p) {
			int n = 0;
			if(p.Length > 0) int.TryParse(p[0], out n);
			n = n > 0 ? n : 0;
			string t = "";
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
		/// Uses input string as format to embed parameter strings.
		/// </summary>
		/// <param name="s">Input string.</param>
		/// <param name="p">parameters.</param>
		/// <returns>Formatted string.</returns>
		private static string Format(string s, string[] p) {
			return string.Format(s, p);
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
			int i = 0, e = s.Length;
			if(p.Length > 0) int.TryParse(p[0], out i);
			if(p.Length > 1) int.TryParse(p[1], out e);
			i = i > s.Length ? s.Length : i;
			while(i < 0) i += s.Length;
			e = e > s.Length ? s.Length : e;
			while(e < 0) e += s.Length;
			return s.Substring(i, e - i);
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
			return s.Remove(i, l);
		}

		/// <summary>
		/// Replace a search string with new string in input string.
		/// </summary>
		/// <param name="s">Input string</param>
		/// <param name="p">search, new.</param>
		/// <returns>Replaced string.</returns>
		private static string Replace(string s, string[] p) {
			string t = GetStr(p, 0, s + " ");
			string u = GetStr(p, 1);
			return s.Replace(t, u);
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
			string t = GetStr(p, 0);
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
		/// Get string from specified index of string array.
		/// </summary>
		/// <param name="a">String array.</param>
		/// <param name="i">Array index.</param>
		/// <param name="v">Optional. Default value.</param>
		/// <returns>String value at specified index, or default value.</returns>
		private static string GetStr(string[] a, int i, string v="") {
			return a.Length > i ? a[i] : v;
		}

		/// <summary>
		/// Get int from specified index of string array.
		/// </summary>
		/// <param name="a">String array.</param>
		/// <param name="i">Array index.</param>
		/// <param name="v">Optional. Default value.</param>
		/// <returns>Int value at specified index, or default value.</returns>
		private static int GetInt(string[] a, int i, int v=0) {
			if(a.Length > i) int.TryParse(a[i], out v);
			return v;
		}
	}
}
