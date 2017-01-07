using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ArrayExpression : Expression {

		private readonly List<Expression> _itemsExp;

		public ArrayExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Array;
			debugInfo = list[position].debugInfo;
			_itemsExp = new List<Expression>();
		}

		public override void Extract(SyntaxContext context) {
			// ignored
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_itemsExp.ToArray());
		}

		public override string ToString() {
			return "{ }";
		}

	}

}