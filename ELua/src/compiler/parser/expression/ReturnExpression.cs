using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ReturnExpression : Expression {

		private Expression _targetExp;

		public ReturnExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Return;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = Extract(context, _targetExp);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_targetExp);
		}

		public override string ToString() {
			return string.Format("return {0}", _targetExp);
		}

	}

}