using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class BindExpression : Expression {

		private Expression _item1Exp;
		private Expression _item2Exp;

		public BindExpression(List<Expression> list, int position, int len) {
			type = Type.Bind;
			debugInfo = list[position].debugInfo;
			IsStatement = true;
			_item1Exp = list[position];
			_item2Exp = list[position + 2];
		}

		public override void Extract(SyntaxContext context) {
			_item1Exp = Extract(context, _item1Exp);
			_item2Exp = Extract(context, _item2Exp);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(_item1Exp, _item2Exp);
		}

		public override string ToString() {
			return string.Format("{0} = {1}", _item1Exp, _item2Exp);
		}

	}

}