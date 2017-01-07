namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NumberExpression : Expression {

		public NumberExpression(string value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.Number;
			IsRightValue = true;
			this.value = value;
			this.debugInfo = debugInfo;
		}

		public override void Generate(ILContext context) {
			context.Add(new IL { opCode = IL.OpCode.Push, arg1 = new LuaNumber { value = float.Parse(value) } });
		}

		public override string ToString() {
			return value;
		}

	}

}