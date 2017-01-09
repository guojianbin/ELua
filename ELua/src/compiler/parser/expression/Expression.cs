using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Expression {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type {

			UnDefine,
			Word, // var
			Keyword, // for,if,else,end...
			Number, // 0.1,11...
			Operator, // +,-,*,/...
			String, // "str", 'str', [[str]]
			Nil, // nil
			Boolean, // true, false
			Label, // xx:

			Paren, // (x)
			Negate, // -x
			Multiply, // x * y
			Division, // x / y
			Plus, // x + y
			Subtract, // x - y
			Mod, // x % y
            Less, // x < y
            Greater, // x > y
            LessEq, // x <= y
            GreaterEq, // x >= y
            Equal, // x == y
            NotEqual, // x ~= y
			Property, // a.x
			Index, // a["x"]
			Call, // a(),b(1)
			List, // {x,y,z}
			Table, // {x=y,z=y}

            Not, // not
            And, // and
            Or, // or

			Module, // stats list
            If, // if end
            IfElse, // if else end
			Define, // local x = y
			Bind, // x = y
			Return // return x

		}

		public bool IsStatement { get; set; }
		public bool IsLeftValue { get; set; }
		public bool IsRightValue { get; set; }
		public bool IsFinally { get; set; }
		public bool IsChunked { get; set; }

		public Type type;
		public DebugInfo debugInfo;

	    public virtual string GetName() {
	        throw new InvalidOperationException(GetExceptionMessage());
	    }

		public virtual void Extract(SyntaxContext context) {
			throw new InvalidOperationException(GetExceptionMessage());
		}

	    public virtual void Generate(ByteCodeContext context) {
			throw new InvalidOperationException(GetExceptionMessage());
		}

		public string GetExceptionMessage() {
			return string.Format("Type={0},DebugInfo={1},Content={2}", GetType().Name, GetDebugInfo(), ToString());
		}

		public virtual string GetDebugInfo() {
			return DebugInfo.ToString(debugInfo);
		}

	}

}