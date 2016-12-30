namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class DefineParser : BaseParser {

		public override bool Parse(Parser parser, int position) {
			this.parser = parser;
			this.position = position;
			if (!item1.IsKeyword("local")) {
				return false;
			}
			parser.Parse(0, level + 1, position + 1);
			if (item2.type != Expression.Type.Bind) {
				return false;
			} else {
				parser.list.Insert(position, new DefineExpression((BindExpression)item2));
				parser.list.RemoveRange(position + 1, 2);
				return true;
			}
		}

	}

}