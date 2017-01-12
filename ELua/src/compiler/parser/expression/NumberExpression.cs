namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NumberExpression : Expression {

        public float value;

        public NumberExpression(string value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.Number;
			IsRightValue = true;
			this.value = float.Parse(value);
			this.debugInfo = debugInfo;
        }

	    public NumberExpression(float value, DebugInfo debugInfo) {
            IsFinally = true;
            type = Type.Number;
            IsRightValue = true;
	        this.value = value;
            this.debugInfo = debugInfo;
        }

	    public override void Generate(ModuleContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = context.vm.GetNumber(value) });
		}

		public override string ToString() {
			return value.ToString();
		}

	}

}