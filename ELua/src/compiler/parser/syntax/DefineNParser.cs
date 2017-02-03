namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class DefineNParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (!ParserHelper.IsKeyword(list[index], "local")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (WordListParser.Parse(context, index));
			if (list[index].type != Expression.Type.WordList) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "=")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (RightList2Parser.Parse(context, index));
			if (list[index].type != Expression.Type.RightList2) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			context.Insert(position, ExpressionCreator.CreateDefineN(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}