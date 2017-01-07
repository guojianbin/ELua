namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class PropertyParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			parser = new ParenParser();
			while (parser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!list[index].IsOperator(".")) {
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