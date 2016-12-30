namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NegateExpression : Expression {

		private Expression _targetExp;

		public NegateExpression(Expression targetExp) {
			IsRightValue = true;
			type = Type.Negate;
			_targetExp = targetExp;
		}

		public override string DebugInfo() {
			return _targetExp.DebugInfo();
		}

		public override string ToString() {
			return string.Format("-{0}", _targetExp);
		}

	}

}