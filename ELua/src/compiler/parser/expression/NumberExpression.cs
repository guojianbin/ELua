namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NumberExpression : Expression {

        public string value;

        public NumberExpression(string value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.Number;
			IsRightValue = true;
			this.value = value;
			this.debugInfo = debugInfo;
		}

		public override void Generate(ByteCodeContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaNumber { value = float.Parse(value) } });
		}

		public override string ToString() {
			return value;
		}

	}

}