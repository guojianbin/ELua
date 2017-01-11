namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NumberExpression : Expression {

        public float value;
	    public LuaNumber numberVar;

        public NumberExpression(string value, DebugInfo debugInfo) {
			IsFinally = true;
			type = Type.Number;
			IsRightValue = true;
			this.value = float.Parse(value);
			this.debugInfo = debugInfo;
            numberVar = new LuaNumber { value = this.value };
        }

	    public NumberExpression(float value, DebugInfo debugInfo) {
            IsFinally = true;
            type = Type.Number;
            IsRightValue = true;
	        this.value = value;
            this.debugInfo = debugInfo;
            numberVar = new LuaNumber { value = this.value };
        }

	    public override void Generate(ModuleContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg1 = numberVar });
		}

		public override string ToString() {
			return value.ToString();
		}

	}

}