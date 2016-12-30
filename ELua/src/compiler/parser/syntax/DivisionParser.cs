namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DivisionParser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsRightValue) {
				return false;
			}
			if (!item2.IsOperator("/")) {
				return false;
			}
			parser.Parse(0, level + 1, position + 2);
			if (!item3.IsRightValue) {
				return false;
			} else {
				parser.list.Insert(position, new DivisionExpression(item1, item3));
				parser.list.RemoveRange(position + 1, 3);
				return true;
			}
		}

	}

}