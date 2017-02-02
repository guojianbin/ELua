namespace ELua {

	/// <summary>
	/// @author Easily
	/// auto generated! don't modify !
	/// </summary>
	public static class ModuleParser {

		public static bool Parse(SyntaxContext context, int position) {
			var list = context.list;
			var offset = 0;
			var index = position;

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
			
			context.Insert(position, ExpressionCreator.CreateModule(list, position, offset));
			context.Remove(position + 1, offset);
			return true;
		}

	}

}