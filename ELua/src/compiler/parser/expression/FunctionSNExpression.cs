using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionSNExpression : Expression {

		public string[] argsList;
		public WordExpression targetExp;
		public WordExpression nameExp;
		public Expression moduleExp;

		public FunctionSNExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			targetExp = (WordExpression)list[position + 1];
			nameExp = (WordExpression)list[position + 3];
			var wordList = list.Skip(position + 5).TakeWhile(t => !ParserHelper.IsOperator(t, ")")).Where(t => t.type == Type.Word);
			argsList = wordList.Cast<WordExpression>().Select(t => t.value).ToArray();
			var itemsList = list.Skip(position + 5 + argsList.Length * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var funcExp = new FunctionANExpression(context.NewUID(), argsList, moduleExp);
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