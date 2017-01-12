namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class NotEqualParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			parser = new ParenParser();
			while (parser.Parse(context, index));
			parser = new FunctionAParser();
			while (parser.Parse(context, index));
			parser = new FunctionANParser();
			while (parser.Parse(context, index));
			parser = new PropertyParser();
			while (parser.Parse(context, index));
			parser = new IndexParser();
			while (parser.Parse(context, index));
			parser = new CallParser();
			while (parser.Parse(context, index));
			parser = new CallNParser();
			while (parser.Parse(context, index));
			parser = new NegateParser();
			while (parser.Parse(context, index));
			parser = new NotParser();
			while (parser.Parse(context, index));
			parser = new MultiplyParser();
			while (parser.Parse(context, index));
			parser = new DivisionParser();
			while (parser.Parse(context, index));
			parser = new ModParser();
			while (parser.Parse(context, index));
			parser = new PlusParser();
			while (parser.Parse(context, index));
			parser = new SubtractParser();
			while (parser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "~")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], "=")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			parser = new ParenParser();
			while (parser.Parse(context, index));
			parser = new FunctionAParser();
			while (parser.Parse(context, index));
			parser = new FunctionANParser();
			while (parser.Parse(context, index));
			parser = new PropertyParser();
			while (parser.Parse(context, index));
			parser = new IndexParser();
			while (parser.Parse(context, index));
			parser = new CallParser();
			while (parser.Parse(context, index));
			parser = new CallNParser();
			while (parser.Parse(context, index));
			parser = new NegateParser();
			while (parser.Parse(context, index));
			parser = new NotParser();
			while (parser.Parse(context, index));
			parser = new MultiplyParser();
			while (parser.Parse(context, index));
			parser = new DivisionParser();
			while (parser.Parse(context, index));
			parser = new ModParser();
			while (parser.Parse(context, index));
			parser = new PlusParser();
			while (parser.Parse(context, index));
			parser = new SubtractParser();
			while (parser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new NotEqualExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}