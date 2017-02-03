namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class WordList2Parser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (list[index].type == Expression.Type.WordList2) {
				return false;
			}
			count = 0;
			while (true) {
			while (WordExParser.Parse(context, index));
			if (context.isMissed) {
				context.isMissed = false;
				offset += 1;
				index = position + offset;
				count += 1;
				break;
			}
			if (list[index].type != Expression.Type.WordEx) {
				break;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			count += 1;
			}
			if (count == 0) {
				return false;
			}
			context.Insert(position, ExpressionCreator.CreateWordList2(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}