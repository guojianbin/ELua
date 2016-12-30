namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class NegateParser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsOperator("-")) {
				return false;
			}
			parser.Parse(0, level + 1, position + 1);
			if (!item2.IsRightValue) {
				return false;
			} else {
				parser.list.Insert(position, new NegateExpression(item2));
				parser.list.RemoveRange(position + 1, 2);
				return true;
			}
		}

	}

}