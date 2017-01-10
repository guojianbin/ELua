using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ParenExpression : Expression {

		public Expression targetExp;

		public ParenExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Paren;
			debugInfo = list[position].debugInfo;
			targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			targetExp = ParserHelper.Extract(context, targetExp);
		}

	    public override void Generate(ModuleContext context) {
            targetExp.Generate(context);
	    }

	    public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return string.Format("({0})", targetExp);
		}

	}

}