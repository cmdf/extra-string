using System.Text.RegularExpressions;

namespace orez.ostring.text {
	class oCasie {


		private static string Casie(string s, string[] p, int n) {
			if (n >= p.Length) return s;
			string[] c = p[n + 1].Split('^');
			string r = p[n], c0 = c[0], c1 = c.Length > 1 ? c[1] : "";
			Match m = r.RegEx().Match(s);
			for (; m.Success; m = m.NextMatch()) {
			}
			return null;
		}

		// regex pattern for recursively matching words and applying below rules:
		// . = keep
		// 1 = upcase
		// 0 = lowcase
		// * = repeaters
		// () = encapsulators
		// ^ = apply rule for non-matched
		// case definition for chaning
		// multi-line operate, input stream operate

	}
}
