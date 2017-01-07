namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WordExpression : Expression {

		public WordExpression(string value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.Word;
			IsLeftValue = true;
			IsRightValue = true;
			this.value = value;
			this.debugInfo = debugInfo;
		}

		public override void Generate(ILContext context) {
			context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaVar { value = value } });
		}

		public override string ToString() {
			return value;
		}

	}

}