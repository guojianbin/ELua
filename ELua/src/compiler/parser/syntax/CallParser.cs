namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class CallParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			parser = new ParenParser();
			while (parser.Parse(context, index));
			parser = new AnonymousFunctionParser();
			while (parser.Parse(context, index));
			parser = new AnonymousFunctionNParser();
			while (parser.Parse(context, index));
			parser = new FunctionParser();
			while (parser.Parse(context, index));
			parser = new FunctionNParser();
			while (parser.Parse(context, index));
			parser = new PropertyParser();
			while (parser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "(")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ")")) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new CallExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}