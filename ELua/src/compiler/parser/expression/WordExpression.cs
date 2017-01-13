namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class WordExpression : Expression {

        public string value;

        public WordExpression(string value, DebugInfo debugInfo) {
            type = Type.Word;
            IsFinally = true;
			IsLeftValue = true;
			IsRightValue = true;
			this.value = value;
			this.debugInfo = debugInfo;
        }

        public override void Generate(ModuleContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = context.vm.GetVar(value) });
		}

		public override string ToString() {
			return value;
		}

	}

}