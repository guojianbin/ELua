namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class PropertyParser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsRightValue) {
				return false;
			}
			if (!item2.IsOperator(".")) {
				return false;
			}
			if (item3.type != Expression.Type.Word) {
				return false;
			} else {
				parser.list.Insert(position, new PropertyExpression(item1, item3));
				parser.list.RemoveRange(position + 1, 3);
				return true;
			}
		}

	}

}