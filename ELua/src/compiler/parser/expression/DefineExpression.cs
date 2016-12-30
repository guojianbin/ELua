namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineExpression : Expression {

		private BindExpression _bindExp;

		public DefineExpression(BindExpression bindExp) {
			IsStatement = true;
			type = Type.Define;
			_bindExp = bindExp;
		}

		public override string DebugInfo() {
			return _bindExp.DebugInfo();
		}

		public override string ToString() {
			return string.Format("local {0}", _bindExp);
		}

	}

}