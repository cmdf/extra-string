using System.Collections.Generic;

namespace orez.ostring.data {
	static class oIDictionaryExt {

		// extension methods
		/// <summary>
		/// Get value associated with key, or default value.
		/// </summary>
		/// <typeparam name="K">Key type.</typeparam>
		/// <typeparam name="V">Value type.</typeparam>
		/// <param name="d">Input dictionary.</param>
		/// <param name="k">Key.</param>
		/// <param name="v">Default value.</param>
		/// <returns></returns>
		public static V GetDef<K, V>(this IDictionary<K, V> d, K k, V v = default(V)) {
			return d.ContainsKey(k) ? d[k] : v;
		}
	}
}
