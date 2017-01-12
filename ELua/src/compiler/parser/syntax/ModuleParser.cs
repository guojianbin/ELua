namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class ModuleParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			IParser parser;

			while (true) {
			parser = new ReturnNParser();
			while (parser.Parse(context, index));
			parser = new ReturnParser();
			while (parser.Parse(context, index));
			parser = new ForNParser();
			while (parser.Parse(context, index));
			parser = new ForParser();
			while (parser.Parse(context, index));
			parser = new ForEachParser();
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
			parser = new DefineNParser();
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

			context.Insert(position, new ModuleExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}