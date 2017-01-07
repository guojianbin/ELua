namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StringExpression : Expression {

		public StringExpression(string value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.String;
			IsRightValue = true;
			this.value = value;
			this.debugInfo = debugInfo;
		}

		public override void Generate(ILContext context) {
			context.Add(new IL { opCode = IL.OpCode.Push, arg1 = new LuaString { value = value } });
		}

		public override string ToString() {
			return value;
		}

	}

}