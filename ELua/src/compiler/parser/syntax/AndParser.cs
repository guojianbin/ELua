namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public static class AndParser {

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
			while (MultiplyParser.Parse(context, index));
			while (DivisionParser.Parse(context, index));
			while (ModParser.Parse(context, index));
			while (AddParser.Parse(context, index));
			while (SubtractParser.Parse(context, index));
			while (ConcatParser.Parse(context, index));
			while (LessParser.Parse(context, index));
			while (GreaterParser.Parse(context, index));
			while (LessEqualParser.Parse(context, index));
			while (GreaterEqualParser.Parse(context, index));
			while (EqualParser.Parse(context, index));
			while (NotEqualParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "and")) {
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
			while (MultiplyParser.Parse(context, index));
			while (DivisionParser.Parse(context, index));
			while (ModParser.Parse(context, index));
			while (AddParser.Parse(context, index));
			while (SubtractParser.Parse(context, index));
			while (ConcatParser.Parse(context, index));
			while (LessParser.Parse(context, index));
			while (GreaterParser.Parse(context, index));
			while (LessEqualParser.Parse(context, index));
			while (GreaterEqualParser.Parse(context, index));
			while (EqualParser.Parse(context, index));
			while (NotEqualParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new AndExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}