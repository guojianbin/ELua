namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public static class ForEachParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;

			if (!ParserHelper.IsKeyword(list[index], "for")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (true) {
			if (list[index].type != Expression.Type.Word) {
				return false;
			}
			if (!list[index].isLeftValue) {
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
			if (!ParserHelper.IsKeyword(list[index], "in")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (true) {
			while (FunctionAParser.Parse(context, index));
			while (FunctionANParser.Parse(context, index));
			while (ParenParser.Parse(context, index));
			while (ListParser.Parse(context, index));
			while (ListNParser.Parse(context, index));
			while (TableSNParser.Parse(context, index));
			while (TableINParser.Parse(context, index));
			while (PropertyParser.Parse(context, index));
			while (IndexParser.Parse(context, index));
			while (CallParser.Parse(context, index));
			while (CallNParser.Parse(context, index));
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
			}
			offset += 1;
			index = position + offset;
			if (!ParserHelper.IsOperator(list[index], ",")) {
				break;
			}
			offset += 1;
			index = position + offset;
			}
			if (!ParserHelper.IsKeyword(list[index], "do")) {
				return false;
			}
			offset += 1;
			index = position + offset;
			while (true) {
			while (ReturnNParser.Parse(context, index));
			while (ReturnParser.Parse(context, index));
			while (BreakParser.Parse(context, index));
			while (DoParser.Parse(context, index));
			while (WhileParser.Parse(context, index));
			while (ForNParser.Parse(context, index));
			while (ForParser.Parse(context, index));
			while (ForEachParser.Parse(context, index));
			while (FunctionNParser.Parse(context, index));
			while (FunctionNNParser.Parse(context, index));
			while (FunctionSParser.Parse(context, index));
			while (FunctionSNParser.Parse(context, index));
			while (IfParser.Parse(context, index));
			while (IfElseParser.Parse(context, index));
			while (DefineParser.Parse(context, index));
			while (DefineNParser.Parse(context, index));
			while (BindParser.Parse(context, index));
			while (BindNParser.Parse(context, index));
			while (CallParser.Parse(context, index));
			while (CallNParser.Parse(context, index));
			if (!list[index].isStatement) {
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

			context.Insert(position, new ForEachExpression(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}