namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class LeftExParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			while (PropertyParser.Parse(context, index));
			while (IndexParser.Parse(context, index));
			if (!list[index].isLeftValue) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ",")) {
				isMissed = true;
				context.isMissed = true;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (isMissed) {
				offset -= 1;
			}
			context.Insert(position, ExpressionCreator.CreateLeftEx(list, position, offset));
			context.Remove(position + 1, offset);
			if (isMissed) {
				return false;
			}
			return true;
		}

	}

}