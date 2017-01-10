namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class FunctionParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			if (!ParserHelper.IsKeyword(list[index], "function")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			if (list[index].type != Expression.Type.Word) {
				return false;
			}
			if (!list[index].IsLeftValue) {
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

			context.Insert(position, new FunctionExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}