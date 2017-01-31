using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ReturnExpression : Expression {

		public ReturnExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Return;
			debugInfo = list[position].debugInfo;
		}

		public override void Extract(SyntaxContext context) {
			// ignored
		}

		public override void Generate(ModuleContext context) {
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Clear });
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Return });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(this);
		}

		public override string ToString() {
			return "return";
		}

	}

}