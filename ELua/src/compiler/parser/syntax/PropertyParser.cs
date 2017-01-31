namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public static class PropertyParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;

			while (ParenParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			while (ListNParser.Parse(context, index));
			while (TableSNParser.Parse(context, index));
			while (TableINParser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ".")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (list[index].type != Expression.Type.Word) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new PropertyExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}