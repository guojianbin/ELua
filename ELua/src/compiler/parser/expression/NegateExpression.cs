using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NegateExpression : Expression {

		public Expression targetExp;

		public NegateExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			type = Type.Negate;
			debugInfo = list[position].debugInfo;
			targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			targetExp = ParserHelper.Extract(context, targetExp);
		}

	    public override void Generate(ModuleContext context) {
            targetExp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Negate });
        }

	    public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return string.Format("-{0}", targetExp);
		}

	}

}