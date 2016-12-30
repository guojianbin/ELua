namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class PriorParser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsOperator("(")) {
				return false;
			}
			parser.Parse(level + 1, parser.parsers.Count, position + 1);
			if (!item2.IsRightValue) {
				return false;
			}
			if (!item3.IsOperator(")")) {
				return false;
			} else {
				parser.list.Insert(position, new PriorExpression(item2));
				parser.list.RemoveRange(position + 1, 3);
				return true;
			}
		}

	}

}