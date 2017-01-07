using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class TableNExpression : Expression {

		private readonly List<KeyValuePair<Expression, Expression>> _itemsList;

		public TableNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Table;
			debugInfo = list[position].debugInfo;
			_itemsList = new List<KeyValuePair<Expression, Expression>>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 4) {
				_itemsList.Add(new KeyValuePair<Expression, Expression>(list[position + i + 1], list[position + i + 3]));
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < _itemsList.Count; i++) {
				var item = _itemsList[i];
				var itemKey = Extract(context, item.Key);
				var itemValue = Extract(context, item.Value);
				_itemsList[i] = new KeyValuePair<Expression, Expression>(itemKey, itemValue);
			}
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_itemsList.Select(t => new[] { t.Key, t.Value }).SelectMany(t => t).ToArray());
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", string.Join(", ", _itemsList.Select(t => string.Format("{0} = {1}", t.Key, t.Value))));
		}

	}

}