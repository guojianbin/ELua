using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CallExpression : Expression {

		public Expression targetExp;

		public CallExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			isStatement = true;
			type = Type.Call;
			debugInfo = list[position].debugInfo;
			targetExp = list[position];
		}

		public override void Extract(SyntaxContext context) {
			targetExp = ParserHelper.Extract(context, targetExp);
		}

		public override void Generate(ModuleContext context) {
			targetExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Call });
        }

		public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return string.Format("{0}()", targetExp);
		}

	}

}