using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CallNExpression : Expression {

		private Expression _targetExp;
		private readonly List<Expression> _argsExp;

		public CallNExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			IsStatement = true;
			type = Type.Call;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position];
			_argsExp = new List<Expression>();
			var argLen = len - 2;
			for (var i = 0; i < argLen; i += 2) {
				_argsExp.Add(list[position + i + 2]);
			}
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = Extract(context, _targetExp);
			for (var i = 0; i < _argsExp.Count; i++) {
				_argsExp[i] = Extract(context, _argsExp[i]);
			}
		}

		public override void Generate(ILContext context) {
			foreach (var item in _argsExp) {
				item.Generate(context);
			}
			context.Add(new IL { opCode = IL.OpCode.Call, arg1 = new LuaVar { value = _targetExp.value } });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(new[] { _targetExp }.Concat(_argsExp).ToArray());
		}

		public override string ToString() {
			return string.Format("{0}({1})", _targetExp, string.Join(", ", _argsExp.Select(t => t.ToString())));
		}

	}

}