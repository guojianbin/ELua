namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class PriorExpression : Expression {

		private Expression _targetExp;

		public PriorExpression(Expression targetExp) {
			IsRightValue = true;
			type = Type.Prior;
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