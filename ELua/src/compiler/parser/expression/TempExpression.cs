namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class TempExpression : Expression {

		public Expression _refExp;

		public TempExpression(string value, Expression refExp) {
			IsFinally = true;
			type = Type.Word;
			this.value = value;
			_refExp = refExp;
			debugInfo = _refExp.debugInfo;
		}

		public override void Generate(ILContext context) {
			context.Add(new IL { opCode = IL.OpCode.Push, arg1 = new LuaVar { value = value } });
		}

		public override string ToString() {
			return value;
		}

	}

}