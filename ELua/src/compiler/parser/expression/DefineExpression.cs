namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineExpression : Expression {

		public Expression item1Exp;
		public Expression item2Exp;

		public DefineExpression(Expression item1Exp, Expression item2Exp) {
			isStatement = true;
            type = Type.Define;
		    debugInfo = item1Exp.debugInfo;
            this.item1Exp = new LocalExpression(item1Exp);
			this.item2Exp = item2Exp;
        }

        public override void Extract(SyntaxContext context) {
            item2Exp = ParserHelper.Extract(context, item2Exp);
        }

        public override void Generate(ModuleContext context) {
			item2Exp.Generate(context);
            item1Exp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(item1Exp, item2Exp);
		}

		public override string ToString() {
			return string.Format("local {0} = {1}", item1Exp, item2Exp);
		}

	}

}