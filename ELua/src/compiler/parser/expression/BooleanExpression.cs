namespace ELua {

    /// <summary>
    /// @author Easily
    /// </summary>
    public class BooleanExpression : Expression {

	    public bool value;

        public BooleanExpression(string value, DebugInfo debugInfo) {
            IsFinally = true;
            type = Type.Boolean;
			IsRightValue = true;
			this.debugInfo = debugInfo;
            this.value = bool.Parse(value);
        }

		public BooleanExpression(bool value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.Boolean;
			IsRightValue = true;
			this.debugInfo = debugInfo;
		    this.value = value;
	    }

	    public override void Generate(ModuleContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = context.vm.GetBoolean(value) });
        }

        public override string ToString() {
            return value.ToString();
        }

    }

}