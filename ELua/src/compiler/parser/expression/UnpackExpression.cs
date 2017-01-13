namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class UnpackExpression : Expression {

		public Expression targetExp;

		public UnpackExpression(Expression targetExp) {
			type = Type.Unpack;
			this.targetExp = targetExp;
		}

		public override void Generate(ModuleContext context) {
			targetExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Unpack });
		}

		public override string ToString() {
			return string.Format("unpack({0})", targetExp);
		}

	}

}