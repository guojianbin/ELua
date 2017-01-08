namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class TempExpression : Expression {

        public string value;
        public Expression _refExp;

		public TempExpression(string value, Expression refExp) {
            type = Type.Word;
            IsFinally = true;
            IsLeftValue = true;
            IsRightValue = true;
            this.value = value;
			_refExp = refExp;
			debugInfo = _refExp.debugInfo;
		}

	    public override string GetName() {
	        return value;
	    }

	    public override void Generate(ILContext context) {
			context.Add(new IL { opCode = IL.OpCode.Push, opArg = new LuaVar { name = value } });
		}

		public override string ToString() {
			return value;
		}

	}

}