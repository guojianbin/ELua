using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ListExpression : Expression {

		public ListExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.List;
			debugInfo = list[position].debugInfo;
		}

		public override void Extract(SyntaxContext context) {
            // ignored
        }

        public override void Generate(ByteCodeContext context) {
            context.Add(new ByteCode { opCode = ByteCode.OpCode.List0 });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(this);
		}

		public override string ToString() {
			return "{ }";
		}

	}

}