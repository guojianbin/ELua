namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class RightList1Parser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (list[index].type == Expression.Type.RightList1) {
				return false;
			}
			while (true) {
			while (RightExParser.Parse(context, index));
			if (context.isMissed) {
				context.isMissed = false;
				offset += 1;
				index = position + offset;
				break;
			}
			if (list[index].type != Expression.Type.RightEx) {
				break;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			}
			context.Insert(position, ExpressionCreator.CreateRightList1(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}