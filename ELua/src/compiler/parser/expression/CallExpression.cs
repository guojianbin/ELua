using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CallExpression : Expression {

		private Expression _targetExp;

		public CallExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			IsStatement = true;
			type = Type.Call;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position];
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = Extract(context, _targetExp);
		}

		public override void Generate(ILContext context) {
			context.Add(new IL { opCode = IL.OpCode.Call, opArg = new LuaFunction { value = _targetExp.GetName() } });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_targetExp);
		}

		public override string ToString() {
			return string.Format("{0}()", _targetExp);
		}

	}

}