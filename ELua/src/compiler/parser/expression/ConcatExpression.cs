using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ConcatExpression : Expression {

		public Expression item1Exp;
		public Expression item2Exp;

		public ConcatExpression(List<Expression> list, int position, int len) {
			type = Type.Concat;
			isRightValue = true;
			debugInfo = list[position].debugInfo;
			item1Exp = list[position];
			item2Exp = list[position + 3];
		}

		public override void Extract(SyntaxContext context) {
			item1Exp = ParserHelper.Extract(context, item1Exp);
			item2Exp = ParserHelper.Extract(context, item2Exp);
		}

		public override void Generate(ModuleContext context) {
			item2Exp.Generate(context);
			item1Exp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Concat });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(item1Exp, item2Exp);
		}

		public override string ToString() {
			return string.Format("{0} .. {1}", item1Exp, item2Exp);
		}

	}

}