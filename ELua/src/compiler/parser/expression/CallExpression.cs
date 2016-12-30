using System.Collections.Generic;
using System.Text;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class CallExpression : Expression {

		private Expression _targetExp;

		public CallExpression(Expression targetExp) {
			IsRightValue = true;
			IsStatement = true;
			type = Type.Call;
			_targetExp = targetExp;
		}

		public override string DebugInfo() {
			return _targetExp.DebugInfo();
		}

		public override string ToString() {
			return string.Format("{0}()", _targetExp);
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Call1Expression : Expression {

		private Expression _targetExp;
		private Expression _argExp;

		public Call1Expression(Expression targetExp, Expression argExp) {
			IsRightValue = true;
			type = Type.Call;
			_targetExp = targetExp;
			_argExp = argExp;
		}

		public override string DebugInfo() {
			return _targetExp.DebugInfo();
		}

		public override string ToString() {
			return string.Format("{0}({1})", _targetExp, _argExp);
		}

	}

	/// <summary>
	/// @author Easily
	/// </summary>
	public class Call9Expression : Expression {

		private Expression _targetExp;
		private List<Expression> _argsExp;

		public Call9Expression(Expression targetExp, List<Expression>  argsExp) {
			IsRightValue = true;
			type = Type.Call;
			_targetExp = targetExp;
			_argsExp = argsExp;
		}

		public override string DebugInfo() {
			return _targetExp.DebugInfo();
		}

		public override string ToString() {
			var sb = new StringBuilder();
			foreach (var argExp in _argsExp) {
				sb.AppendFormat("{0}, ", argExp);
			}
			return string.Format("{0}({1})", _targetExp, sb.ToString(0, sb.Length - 2));
		}

	}

}