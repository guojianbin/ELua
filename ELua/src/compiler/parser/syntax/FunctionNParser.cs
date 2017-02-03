namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class FunctionNParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (!ParserHelper.IsKeyword(list[index], "function")) {
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
			if (!ParserHelper.IsOperator(list[index], "(")) {
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
			if (!ParserHelper.IsOperator(list[index], ")")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (ModuleParser.Parse(context, index));
			if (list[index].type != Expression.Type.Module) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "end")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			context.Insert(position, ExpressionCreator.CreateFunctionN(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}