namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class StringExpression : Expression {

        public string value;

        public StringExpression(string value, DebugInfo debugInfo) {
			isFinally = true;
			type = Type.String;
			isRightValue = true;
			this.value = value;
			this.debugInfo = debugInfo;
		}

		public override void Generate(ModuleContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = context.vm.GetString(value) });
		}

		public override string ToString() {
            return string.Format("\"{0}\"", value);
        }

	}

}