namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class VarExpression : Expression {

        public string value;
        public Expression refExp;

		public VarExpression(string value, Expression refExp) {
            type = Type.Word;
            IsFinally = true;
            IsLeftValue = true;
            IsRightValue = true;
            this.value = value;
			this.refExp = refExp;
			debugInfo = this.refExp.debugInfo;
		}

	    public override string GetName() {
	        return value;
	    }

	    public override void Generate(ByteCodeContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Push, opArg = new LuaVar { name = value } });
		}

		public override string ToString() {
			return value;
		}

	}

}