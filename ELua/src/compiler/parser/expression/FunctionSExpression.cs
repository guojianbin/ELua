using System.Collections.Generic;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionSExpression : Expression {

		public string[] argsList;
		public WordExpression targetExp;
		public WordExpression nameExp;
		public Expression moduleExp;

		public FunctionSExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			targetExp = (WordExpression)list[position + 1];
			nameExp = (WordExpression)list[position + 3];
			argsList = ((WordList1Expression)list[position + 5]).ToParams();
			moduleExp = list[position + 7];
        }

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var funcExp = new FunctionAExpression(context.NewUID(), argsList, moduleExp);
			var propExp = new PropertyExpression(targetExp, ParserHelper.Word2String(nameExp));
			var bindExp = new BindExpression(propExp, funcExp);
			bindExp.Generate(context);
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}.{1}({2})\n{3}\nend", targetExp, nameExp, argsList.FormatListString(), moduleExp);
		}

	}

}