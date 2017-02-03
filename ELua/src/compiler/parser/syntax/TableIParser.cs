namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class TableIParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (!ParserHelper.IsOperator(list[index], "{")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (KVList1Parser.Parse(context, index));
			if (list[index].type != Expression.Type.KVList1) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "}")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			context.Insert(position, ExpressionCreator.CreateTableI(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}