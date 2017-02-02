namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class ReturnParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;

			if (!ParserHelper.IsKeyword(list[index], "return")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			
			context.Insert(position, ExpressionCreator.CreateReturn(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}