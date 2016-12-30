namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class EOSExpression : Expression {

		public EOSExpression() {
			IsStatement = true;
		}

		public override string DebugInfo() {
			return ToString();
		}

		public override string ToString() {
			return "<END>";
		}

	}

}