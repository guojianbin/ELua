using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CallNExpression : Expression {

		private Expression _targetExp;
		private readonly List<Expression> _itemsList;

		public CallNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			IsStatement = true;
			type = Type.Call;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position];
			_itemsList = new List<Expression>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 2) {
				_itemsList.Add(list[position + i + 2]);
			}
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = ParserHelper.Extract(context, _targetExp);
			for (var i = 0; i < _itemsList.Count; i++) {
				_itemsList[i] = ParserHelper.Extract(context, _itemsList[i]);
			}
		}

		public override void Generate(ILContext context) {
		    for (int i = _itemsList.Count - 1; i >= 0; i--) {
		        _itemsList[i].Generate(context);
		    }
			context.Add(new IL { opCode = IL.OpCode.Call, opArg = new LuaVar { value = _targetExp.GetName() } });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(new[] { _targetExp }.Concat(_itemsList).ToArray());
		}

		public override string ToString() {
			return string.Format("{0}({1})", _targetExp, string.Join(", ", _itemsList.Select(t => t.ToString())));
		}

	}

}