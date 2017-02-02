namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class InvokeParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;

			while (ParenParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			while (ListNParser.Parse(context, index));
			while (TableSNParser.Parse(context, index));
			while (TableINParser.Parse(context, index));
			while (PropertyParser.Parse(context, index));
			while (IndexParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "(")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (true) {
			while (RightExParser.Parse(context, index));
			if (list[index].type != Expression.Type.RightEx) {
				break;
			}
			offset += 1;
			index = position + offset;
			}
			if (!ParserHelper.IsOperator(list[index], ")")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			
			context.Insert(position, ExpressionCreator.CreateInvoke(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}