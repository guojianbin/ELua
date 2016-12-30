namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Token {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type {

			Word,			// e.g. var
			Keyword,		// e.g. for,if,else,end...
			Number,		// e.g. 0.1,11...
			Operator,		// e.g. +,-,*,/...
			String			// e.g. "str", 'str', [[str]]

		}

		public Type type;
		public string value;
		public int index;

		public int line;
		public int position;
		public string file;

		public override string ToString() {
			return string.Format("Type: {0}, Value: {1}, Index: {2}, Line: {3}, Position: {4}, File: {5}", type, value, index, line, position, file);
		}

	}

}