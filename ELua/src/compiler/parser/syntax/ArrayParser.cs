namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class ArrayParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			if (!list[index].IsOperator("{")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!list[index].IsOperator("}")) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new ArrayExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}