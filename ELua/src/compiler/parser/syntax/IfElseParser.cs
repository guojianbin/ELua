namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class IfElseParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			if (!ParserHelper.IsKeyword(list[index], "if")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			parser = new ParenParser();
			while (parser.Parse(context, index));
			parser = new AnonymousFunctionParser();
			while (parser.Parse(context, index));
			parser = new AnonymousFunctionNParser();
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
			parser = new TableParser();
			while (parser.Parse(context, index));
			parser = new TableNParser();
			while (parser.Parse(context, index));
			if (!list[index].IsRightValue) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "then")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (true) {
			parser = new ReturnNParser();
			while (parser.Parse(context, index));
			parser = new ReturnParser();
			while (parser.Parse(context, index));
			parser = new ForNParser();
			while (parser.Parse(context, index));
			parser = new ForParser();
			while (parser.Parse(context, index));
			parser = new FunctionParser();
			while (parser.Parse(context, index));
			parser = new FunctionNParser();
			while (parser.Parse(context, index));
			parser = new IfParser();
			while (parser.Parse(context, index));
			parser = new IfElseParser();
			while (parser.Parse(context, index));
			parser = new DefineParser();
			while (parser.Parse(context, index));
			parser = new BindParser();
			while (parser.Parse(context, index));
			parser = new CallParser();
			while (parser.Parse(context, index));
			parser = new CallNParser();
			while (parser.Parse(context, index));
			if (!list[index].IsStatement) {
				break;
			}
			offset += 1;
			index = position + offset;
			}
			if (!ParserHelper.IsKeyword(list[index], "else")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (true) {
			parser = new ReturnNParser();
			while (parser.Parse(context, index));
			parser = new ReturnParser();
			while (parser.Parse(context, index));
			parser = new ForNParser();
			while (parser.Parse(context, index));
			parser = new ForParser();
			while (parser.Parse(context, index));
			parser = new FunctionParser();
			while (parser.Parse(context, index));
			parser = new FunctionNParser();
			while (parser.Parse(context, index));
			parser = new IfParser();
			while (parser.Parse(context, index));
			parser = new IfElseParser();
			while (parser.Parse(context, index));
			parser = new DefineParser();
			while (parser.Parse(context, index));
			parser = new BindParser();
			while (parser.Parse(context, index));
			parser = new CallParser();
			while (parser.Parse(context, index));
			parser = new CallNParser();
			while (parser.Parse(context, index));
			if (!list[index].IsStatement) {
				break;
			}
			offset += 1;
			index = position + offset;
			}
			if (!ParserHelper.IsKeyword(list[index], "end")) {
				return false;
			}
			offset += 1;
			index = position + offset;

			context.Insert(position, new IfElseExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}