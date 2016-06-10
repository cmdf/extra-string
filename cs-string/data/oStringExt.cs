using System.Text;

namespace orez.ostring.data {
	static class oStringExt {

		// extension methods
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
