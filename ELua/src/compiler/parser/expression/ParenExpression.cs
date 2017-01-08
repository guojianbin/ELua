using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ParenExpression : Expression {

		private Expression _targetExp;

		public ParenExpression(List<Expression> list, int position, int len) {
			IsRightValue = true;
			type = Type.Paren;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position + 1];
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = ParserHelper.Extract(context, _targetExp);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_targetExp);
		}

		public override string ToString() {
			return string.Format("({0})", _targetExp);
		}

	}

}