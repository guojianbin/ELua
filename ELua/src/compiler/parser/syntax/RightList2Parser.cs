namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class RightList2Parser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (list[index].type == Expression.Type.RightList2) {
				return false;
			}
			count = 0;
			while (true) {
			while (RightExParser.Parse(context, index));
			if (context.isMissed) {
				context.isMissed = false;
				offset += 1;
				index = position + offset;
				count += 1;
				break;
			}
			if (list[index].type != Expression.Type.RightEx) {
				break;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			count += 1;
			}
			if (count == 0) {
				return false;
			}
			context.Insert(position, ExpressionCreator.CreateRightList2(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}