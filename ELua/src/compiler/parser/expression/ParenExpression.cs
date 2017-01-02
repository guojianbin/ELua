namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class ParenExpression : Expression {

		private Expression _targetExp;

		public ParenExpression(Expression targetExp) {
			IsRightValue = true;
			type = Type.Paren;
			_targetExp = targetExp;
		}

		public override string DebugInfo() {
			return _targetExp.DebugInfo();
		}

		public override string ToString() {
			return string.Format("({0})", _targetExp);
		}

	}

}