namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class IfCondParser : IParser {

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
			parser = new LessEqParser();
			while (parser.Parse(context, index));
			parser = new GreaterEqParser();
			while (parser.Parse(context, index));
			parser = new EqualParser();
			while (parser.Parse(context, index));
			parser = new NotEqualParser();
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
			parser = new ReturnParser();
			while (parser.Parse(context, index));
			parser = new IfCondParser();
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

			context.Insert(position, new IfCondExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}