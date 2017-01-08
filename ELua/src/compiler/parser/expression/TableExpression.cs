using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class TableExpression : Expression {

		private readonly List<KeyValuePair<Expression, Expression>> _itemsList;

		public TableExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Table;
			debugInfo = list[position].debugInfo;
			_itemsList = new List<KeyValuePair<Expression, Expression>>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 4) {
			    var keyExp = (WordExpression)list[position + i + 1];
			    var itemKey = new StringExpression(keyExp.value, keyExp.debugInfo);
			    var itemValue = list[position + i + 3];
			    _itemsList.Add(new KeyValuePair<Expression, Expression>(itemKey, itemValue));
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

        public override void Generate(ILContext context) {
            for (var i = _itemsList.Count - 1; i >= 0; i--) {
                var item = _itemsList[i];
                item.Value.Generate(context);
                item.Key.Generate(context);
            }
            context.Add(new IL { opCode = IL.OpCode.Table });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(_itemsList.Select(t => new[] { t.Key, t.Value }).SelectMany(t => t).ToArray());
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", string.Join(", ", _itemsList.Select(t => string.Format("{0} = {1}", t.Key, t.Value))));
		}

	}

}