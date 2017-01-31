namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public static class ListParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;

			if (!ParserHelper.IsOperator(list[index], "{")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "}")) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new ListExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}