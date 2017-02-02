namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class FunctionNNParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;

			if (!ParserHelper.IsKeyword(list[index], "function")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (list[index].type != Expression.Type.Word) {
				return false;
			}
			if (!list[index].isLeftValue) {
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
			if (list[index].type != Expression.Type.Word) {
				return false;
			}
			if (!list[index].isLeftValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ",")) {
				break;
			}
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ")")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (ModuleParser.Parse(context, index));
			if (list[index].type != Expression.Type.Module) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "end")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			
			context.Insert(position, ExpressionCreator.CreateFunctionNN(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}