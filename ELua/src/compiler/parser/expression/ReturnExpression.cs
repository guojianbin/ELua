using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ReturnExpression : Expression {

		public Expression paramsExp;

		public ReturnExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Return;
			debugInfo = list[position].debugInfo;
			paramsExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			paramsExp.Extract(context);
        }

        public override void Generate(ModuleContext context) {
			paramsExp.Generate(context);
            context.Add(new ByteCode { opCode = ByteCode.OpCode.Return });
        }

		public override string GetDebugInfo() {
			return DebugInfo.ToString(paramsExp);
		}

		public override string ToString() {
			return string.Format("return {0}", paramsExp);
		}

	}

}