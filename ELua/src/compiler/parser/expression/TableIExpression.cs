using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class TableIExpression : Expression {

		public Expression itemsList;

		public TableIExpression(List<Expression> list, int position, int len) {
			isRightValue = true;
			type = Type.Table;
			debugInfo = list[position].debugInfo;
			itemsList = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			itemsList.Extract(context);
        }

        public override void Generate(ModuleContext context) {
			itemsList.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Table });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(itemsList);
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", itemsList);
		}

	}

}