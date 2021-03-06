namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public static class ModuleParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;
			var count = 0;
			var isMissed = false;

			count = 0;
			while (true) {
			while (ReturnParser.Parse(context, index));
			while (BreakParser.Parse(context, index));
			while (DoParser.Parse(context, index));
			while (WhileParser.Parse(context, index));
			while (ForNParser.Parse(context, index));
			while (ForParser.Parse(context, index));
			while (ForEachParser.Parse(context, index));
			while (FunctionNParser.Parse(context, index));
			while (FunctionSParser.Parse(context, index));
			while (IfParser.Parse(context, index));
			while (IfElseParser.Parse(context, index));
			while (DefineNParser.Parse(context, index));
			while (CallParser.Parse(context, index));
			while (BindParser.Parse(context, index));
			while (BindNParser.Parse(context, index));
			if (context.isMissed) {
				context.isMissed = false;
				offset += 1;
				index = position + offset;
				count += 1;
				break;
			}
			if (!list[index].isStatement) {
				break;
			} else {
				// ignored
			}
			offset += 1;
			index = position + offset;
			count += 1;
			}
			if (count == 0) {
				return false;
			}
			context.Insert(position, ExpressionCreator.CreateModule(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}