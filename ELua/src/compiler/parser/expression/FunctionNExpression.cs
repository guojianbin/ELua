using System.Collections.Generic;
using System.Linq;

namespace ELua {

	/// <summary>
	/// @author Easily
	/// </summary>
	public class FunctionNExpression : Expression {

		public string[] argsList;
		public WordExpression nameExp;
		public Expression moduleExp;

		public FunctionNExpression(List<Expression> list, int position, int len) {
			IsStatement = true;
			type = Type.Function;
			debugInfo = list[position].debugInfo;
			nameExp = (WordExpression)list[position + 1];
			var wordList = list.Skip(position + 3).TakeWhile(t => !ParserHelper.IsOperator(t, ")")).Where(t => t.type == Type.Word);
			argsList = wordList.Cast<WordExpression>().Select(t => t.value).ToArray();
			var itemsList = list.Skip(position + 3 + argsList.Length * 2).TakeWhile(t => !ParserHelper.IsKeyword(t, "end")).ToList();
			moduleExp = new ModuleExpression(itemsList);
		}

		public override void Extract(SyntaxContext context) {
			moduleExp.Extract(context);
		}

		public override void Generate(ModuleContext context) {
			var module = new Module(new ModuleContext(context.vm, nameExp.value, context.level + 1) { argsList = argsList });
			context.vm.Add(module);
			moduleExp.Generate(context.Bind(nameExp.value, module.context));
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Function, opArg1 = new LuaModule(context.vm, module) });
			nameExp.Generate(context);
			context.Add(new ByteCode { opCode = ByteCode.OpCode.Bind });
		}

		public override string GetDebugInfo() {
			return DebugInfo.ToString(moduleExp);
		}

		public override string ToString() {
			return string.Format("function {0}()\n{1}\nend", nameExp, moduleExp);
		}

	}

}