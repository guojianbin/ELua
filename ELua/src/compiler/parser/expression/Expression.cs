namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Expression {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type {

			Word,			// e.g. var
			Keyword,		// e.g. for,if,else,end...
			Number,		    // e.g. 0.1,11...
			Operator,		// e.g. +,-,*,/...
			String,		    // e.g. "str", 'str', [[str]]

			Negate,		    // -x
			Paren,			// (x)
			Add,			// x + y
			Subtract,		// x - y
			Multiply,		// x * y
			Division,		// x / y
			Mod,			// x % y
			Property,		// e.g. a.x
			Index,			// e.g. a["x"]
			Call,			// e.g. a(),b(1)

			Define,		// local x = y
			Bind,			// x = y

		}

		public Token token;
		public Type type;

		public bool IsToken { get; set; }
		public bool IsStatement { get; set; }
		public bool IsLeftValue { get; set; }
		public bool IsRightValue { get; set; }

		public bool IsOperator(string value) {
			return type == Type.Operator && token.value == value;
		}

		public bool IsKeyword(string value) {
			return type == Type.Keyword && token.value == value;
		}

		public virtual string DebugInfo() {
			return string.Format("line:{0}", token.line);
		}

		public override string ToString() {
			if (type == Type.String) {
				return string.Format("\"{0}\"", token.value);
			} else {
				return token.value;
			}
		}

	}

}