using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ListNExpression : Expression {

		private readonly List<Expression> _itemsList;

		public ListNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.List;
			debugInfo = list[position].debugInfo;
			_itemsList = new List<Expression>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 2) {
				_itemsList.Add(list[position + i + 1]);
			}
		}

		public override void Extract(SyntaxContext context) {
			for (var i = 0; i < _itemsList.Count; i++) {
				_itemsList[i] = Extract(context, _itemsList[i]);
			}
        }

        public override void Generate(ILContext context) {
            for (var i = _itemsList.Count - 1; i >= 0; i--) {
                _itemsList[i].Generate(context);
            }
            context.Add(new IL { opCode = IL.OpCode.List });
        }

        public override string GetDebugInfo() {
			return DebugInfo.ToString(_itemsList.ToArray());
		}

		public override string ToString() {
			return string.Format("{{ {0} }}", string.Join(", ", _itemsList.Select(t => t.ToString())));
		}

	}

}