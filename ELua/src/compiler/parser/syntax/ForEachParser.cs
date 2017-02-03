namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class ForEachParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (!ParserHelper.IsKeyword(list[index], "for")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (LeftList2Parser.Parse(context, index));
			if (list[index].type != Expression.Type.LeftList2) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "in")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (RightList2Parser.Parse(context, index));
			if (list[index].type != Expression.Type.RightList2) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "do")) {
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
			context.Insert(position, ExpressionCreator.CreateForEach(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}