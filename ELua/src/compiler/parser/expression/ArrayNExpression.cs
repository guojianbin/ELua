using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ArrayNExpression : Expression {

		private readonly List<Expression> _itemsExp;

		public ArrayNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Array;
			debugInfo = list[position].debugInfo;
			_itemsExp = new List<Expression>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 2) {
				_itemsExp.Add(list[position + i + 1]);
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < _itemsExp.Count; i++) {
				_itemsExp[i] = Extract(context, _itemsExp[i]);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_itemsExp.ToArray());
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", string.Join(", ", _itemsExp.Select(t => t.ToString())));
		}

	}

}