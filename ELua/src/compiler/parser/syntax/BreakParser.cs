namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class BreakParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			if (!ParserHelper.IsKeyword(list[index], "break")) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new BreakExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}