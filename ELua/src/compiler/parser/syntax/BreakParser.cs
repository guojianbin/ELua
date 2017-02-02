namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class BreakParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;

			if (!ParserHelper.IsKeyword(list[index], "break")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			
			context.Insert(position, ExpressionCreator.CreateBreak(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}