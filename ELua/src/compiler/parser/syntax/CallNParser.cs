namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class CallNParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			parser = new ParenParser();
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
			while (true) {
			parser = new FunctionAParser();
			while (parser.Parse(context, index));
			parser = new FunctionANParser();
			while (parser.Parse(context, index));
			parser = new ParenParser();
			while (parser.Parse(context, index));
			parser = new PropertyParser();
			while (parser.Parse(context, index));
			parser = new IndexParser();
			while (parser.Parse(context, index));
			parser = new CallParser();
			while (parser.Parse(context, index));
			parser = new CallNParser();
			while (parser.Parse(context, index));
			parser = new ConcatParser();
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
			parser = new LessParser();
			while (parser.Parse(context, index));
			parser = new GreaterParser();
			while (parser.Parse(context, index));
			parser = new LessEqualParser();
			while (parser.Parse(context, index));
			parser = new GreaterEqualParser();
			while (parser.Parse(context, index));
			parser = new EqualParser();
			while (parser.Parse(context, index));
			parser = new NotEqualParser();
			while (parser.Parse(context, index));
			parser = new AndParser();
			while (parser.Parse(context, index));
			parser = new OrParser();
			while (parser.Parse(context, index));
			parser = new ListParser();
			while (parser.Parse(context, index));
			parser = new ListNParser();
			while (parser.Parse(context, index));
			parser = new TableNParser();
			while (parser.Parse(context, index));
			parser = new TableN2Parser();
			while (parser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ",")) {
				break;
			}
			offset += 1;
			index = position + offset;
			}
			if (!ParserHelper.IsOperator(list[index], ")")) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new CallNExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}