using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ReturnExpression : Expression {

		public Expression targetExp;

		public ReturnExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Return;
			debugInfo = list[position].debugInfo;
			targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			targetExp = ParserHelper.Extract(context, targetExp);
        }

        public override void Generate(ByteCodeContext context) {
            targetExp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Ret });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(targetExp);
		}

		public override string ToString() {
			return string.Format("return {0}", targetExp);
		}

	}

}