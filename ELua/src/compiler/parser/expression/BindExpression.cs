namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class BindExpression : Expression {

		private Expression _leftExp;
		private Expression _rightExp;

		public BindExpression(Expression leftExp, Expression rightExp) {
			IsStatement = true;
			type = Type.Bind;
			_leftExp = leftExp;
			_rightExp = rightExp;
		}

		public override string DebugInfo() {
			return string.Format("{0}, {1}", _leftExp.DebugInfo(), _rightExp.DebugInfo());
		}

		public override string ToString() {
			return string.Format("{0} = {1}", _leftExp, _rightExp);
		}

	}

}