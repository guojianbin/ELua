namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class IfElseParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			if (!ParserHelper.IsKeyword(list[index], "if")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (FunctionAParser.Parse(context, index));
			while (ParenParser.Parse(context, index));
			while (TableIParser.Parse(context, index));
			while (TableSParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			while (PropertyParser.Parse(context, index));
			while (IndexParser.Parse(context, index));
			while (CallParser.Parse(context, index));
			while (NotParser.Parse(context, index));
			while (LengthParser.Parse(context, index));
			while (NegateParser.Parse(context, index));
			while (PowerParser.Parse(context, index));
			while (MultiplyParser.Parse(context, index));
			while (DivisionParser.Parse(context, index));
			while (ModParser.Parse(context, index));
			while (AddParser.Parse(context, index));
			while (SubtractParser.Parse(context, index));
			while (ConcatParser.Parse(context, index));
			while (LessParser.Parse(context, index));
			while (GreaterParser.Parse(context, index));
			while (LessEqualParser.Parse(context, index));
			while (GreaterEqualParser.Parse(context, index));
			while (EqualParser.Parse(context, index));
			while (NotEqualParser.Parse(context, index));
			while (AndParser.Parse(context, index));
			while (OrParser.Parse(context, index));
			if (!list[index].isRightValue) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "then")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (ModuleParser.Parse(context, index));
			if (list[index].type != Expression.Type.Module) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "else")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			while (ModuleParser.Parse(context, index));
			if (list[index].type != Expression.Type.Module) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsKeyword(list[index], "end")) {
				return false;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			context.Insert(position, ExpressionCreator.CreateIfElse(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}