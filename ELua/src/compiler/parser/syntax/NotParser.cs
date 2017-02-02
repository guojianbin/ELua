namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class NotParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;

			if (!ParserHelper.IsKeyword(list[index], "not")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (ParenParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			while (ListNParser.Parse(context, index));
			while (TableSNParser.Parse(context, index));
			while (TableINParser.Parse(context, index));
			while (PropertyParser.Parse(context, index));
			while (IndexParser.Parse(context, index));
			while (CallParser.Parse(context, index));
			while (CallNParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			
			context.Insert(position, ExpressionCreator.CreateNot(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}