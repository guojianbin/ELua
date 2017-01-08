namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public class ModuleParser : IParser {

		public bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			while (true) {
				if (position >= list.Count - 1) {
					return true;
				}
				IParser parser;
				parser = new DefineParser();
				if (parser.Parse(context, position)) {
					position += 1;
					if (position < list.Count) {
						continue;
					} else {
						return true;
					}
				}
				parser = new BindParser();
				if (parser.Parse(context, position)) {
					position += 1;
					if (position < list.Count) {
						continue;
					} else {
						return true;
					}
				}
				parser = new CallParser();
				if (parser.Parse(context, position)) {
					position += 1;
					if (position < list.Count) {
						continue;
					} else {
						return true;
					}
				}
				parser = new CallNParser();
				if (parser.Parse(context, position)) {
					position += 1;
					if (position < list.Count) {
						continue;
					} else {
						return true;
					}
				}
				parser = new ReturnParser();
				if (parser.Parse(context, position)) {
					position += 1;
					if (position < list.Count) {
						continue;
					} else {
						return true;
					}
				}
				position += 1;
			}
		}

	}

}