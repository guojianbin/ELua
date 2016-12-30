namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class SubtractExpression : Expression {

		private Expression _item1Exp;
		private Expression _item2Exp;

		public SubtractExpression(Expression item1Exp, Expression item2Exp) {
			IsRightValue = true;
			type = Type.Subtract;
			_item1Exp = item1Exp;
			_item2Exp = item2Exp;
		}

		public override string DebugInfo() {
			return string.Format("{0}, {1}", _item1Exp.DebugInfo(), _item2Exp.DebugInfo());
		}

		public override string ToString() {
			return string.Format("{0} - {1}", _item1Exp, _item2Exp);
		}

	}

}