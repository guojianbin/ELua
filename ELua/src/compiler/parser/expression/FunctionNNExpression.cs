using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionNNExpression : Expression {

		public string[] argsList;
		public WordExpression nameExp;
		public Expression moduleExp;

		public FunctionNNExpression(List<Expression> list, int position, int len) {
			isStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			nameExp = (WordExpression)list[position + 1];
			var wordList = list.Skip(position + 3).TakeWhile(t => !ParserHelper.IsOperator(t, ")")).Where(t => t.type == Type.Word);
			argsList = wordList.Cast<WordExpression>().Select(t => t.value).ToArray();
            moduleExp = list[position + 3 + argsList.Length * 2];
        }

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var module = new Module(new ModuleContext(context.vm, nameExp.value, context.level + 1) { argsList = argsList });
			context.vm.Add(module);
			moduleExp.Generate(module.context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg = new LuaModule(context.vm, module) });
			nameExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}({1})\n{2}\nend", nameExp, argsList.FormatListString(), moduleExp);
		}

	}

}