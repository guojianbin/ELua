namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public static class PowerParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;

			while (ParenParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			while (ListNParser.Parse(context, index));
			while (TableSNParser.Parse(context, index));
			while (TableINParser.Parse(context, index));
			while (PropertyParser.Parse(context, index));
			while (IndexParser.Parse(context, index));
			while (CallParser.Parse(context, index));
			while (CallNParser.Parse(context, index));
			while (NotParser.Parse(context, index));
			while (LengthParser.Parse(context, index));
			while (NegateParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "^")) {
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
			while (NotParser.Parse(context, index));
			while (LengthParser.Parse(context, index));
			while (NegateParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new PowerExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}