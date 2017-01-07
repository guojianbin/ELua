using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class IndexExpression : Expression {

		private Expression _targetExp;
		private Expression _argExp;

		public IndexExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			IsLeftValue = true;
			IsRightValue = true;
			type = Type.Index;
			debugInfo = list[position].debugInfo;
			_targetExp = list[position];
			_argExp = list[position + 2];
		}

		public override void Extract(SyntaxContext context) {
			_targetExp = Extract(context, _targetExp);
			_argExp = Extract(context, _argExp);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_targetExp, _argExp);
		}

		public override string ToString() {
			return string.Format("{0}[{1}]", _targetExp, _argExp);
		}

	}

}