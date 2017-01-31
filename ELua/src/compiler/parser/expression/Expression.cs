using System;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Expression {

		/// <summary>
		/// @author Easily
		/// </summary>
		public enum Type : byte {

			Undef,
			Word, // var
			Keyword, // for,if,else,end...
			Number, // 0.1,11...
			Operator, // +,-,*,/...
			String, // "str", 'str', [[str]]
			Nil, // nil
			Boolean, // true, false
			Label, // xx:
			Break, // break

			Paren, // (x)
			Negate, // -x
			Length, // #x
			Multiply, // x * y
			Division, // x / y
			Add, // x + y
			Subtract, // x - y
			Mod, // x % y
            Less, // x < y
            Greater, // x > y
            LessEqual, // x <= y
            GreaterEqual, // x >= y
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
			For, // for i=x,y,z do ... end
			ForEach, // for x,y in list do ... end
            While, // while do
            Loop, // while true 
            If, // if end
            IfElse, // if else end
			Define, // local x = y
			Bind, // x = y
			Concat, // x .. y
			Return, // return x
			Function, // function x() end
			Unpack, // unpack(x) -> x,y,z

		}

		public bool IsStatement { get; set; }
		public bool IsLeftValue { get; set; }
		public bool IsRightValue { get; set; }
		public bool IsFinally { get; set; }
		public bool IsModule { get; set; }

		public Type type;
		public DebugInfo debugInfo;

		public virtual void Extract(SyntaxContext context) {
			throw new InvalidOperationException(GetExceptionMessage());
		}

	    public virtual void Generate(ModuleContext context) {
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