namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class PropertyParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			while (ParenParser.Parse(context, index));
			while (TableIParser.Parse(context, index));
			while (TableSParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ".")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (list[index].type != Expression.Type.Word) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			context.Insert(position, ExpressionCreator.CreateProperty(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}