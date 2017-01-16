namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public struct Token {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type {

			Undef,
			Word,			// e.g. var
			Keyword,		// e.g. for,if,else,end...
			Number,		// e.g. 0.1,11...
			Operator,		// e.g. +,-,*,/...
			String			// e.g. "str", 'str', [[str]]

		}

		public Type type;
		public string value;
		public int index;

		public DebugInfo debugInfo;

		public override string ToString() {
			return string.Format("Type: {0}, Value: {1}, Index: {2}, DebugInfo: {3}", type, value, index, debugInfo);
		}

	}

}