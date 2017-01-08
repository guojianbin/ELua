using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ListExpression : Expression {

		private List<Expression> _itemsList;

		public ListExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.List;
			debugInfo = list[position].debugInfo;
			_itemsList = new List<Expression>();
		}

		public override void Extract(SyntaxContext context) {
            // ignored
        }

        public override void Generate(ILContext context) {
            context.Add(new IL { opCode = IL.OpCode.List0 });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(_itemsList.ToArray());
		}

		public override string ToString() {
			return "{ }";
		}

	}

}